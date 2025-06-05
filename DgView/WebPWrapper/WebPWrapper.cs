using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using Cairo;
using DgView.Chaek;
// ReSharper disable InconsistentNaming

namespace DgView.WebPWrapper;

internal static class WebP
{
    #region | Public Decode Functions |

    /// <summary>Read a WebP file</summary>
    /// <param name="pathFileName">WebP file to load</param>
    /// <returns>Bitmap with the WebP image</returns>
    public static ImageSurface Load(string pathFileName)
    {
        try
        {
            var rawWebP = File.ReadAllBytes(pathFileName);

            return Decode(rawWebP);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message + "\r\nIn WebP.Load");
        }
    }

    /// <summary>Decode a WebP image</summary>
    /// <param name="rawWebP">The data to uncompress</param>
    /// <param name="imgWidth"></param>
    /// <param name="imgHeight"></param>
    /// <param name="hasAlpha"></param>
    /// <returns>Bitmap with the WebP image</returns>
    public static ImageSurface Decode(byte[] rawWebP, int imgWidth, int imgHeight, bool hasAlpha)
    {
        ImageSurface? bmp = null;
        var pinnedWebP = GCHandle.Alloc(rawWebP, GCHandleType.Pinned);
        var ptrData = pinnedWebP.AddrOfPinnedObject();

        try
        {
            if (!hasAlpha)
            {
                bmp = new ImageSurface(Format.RGB24, imgWidth, imgHeight);
                var outputSize = imgHeight * bmp.Stride;
                UnsafeNativeMethods.WebPDecodeBGRInto(ptrData, (nuint)rawWebP.Length, bmp.DataPtr, outputSize, bmp.Stride);
            }
            else
            {
                bmp = new ImageSurface(Format.ARGB32, imgWidth, imgHeight);
                var outputSize = imgHeight * bmp.Stride;
                UnsafeNativeMethods.WebPDecodeBGRAInto(ptrData, (nuint)rawWebP.Length, bmp.DataPtr, outputSize, bmp.Stride);
            }

            return bmp;
        }
        finally
        {
            bmp?.MarkDirty();

            //Free memory
            if (pinnedWebP.IsAllocated)
                pinnedWebP.Free();
        }
    }

    /// <summary>Decode a WebP image</summary>
    /// <param name="rawWebP">The data to uncompress</param>
    /// <returns>Bitmap with the WebP image</returns>
    public static ImageSurface Decode(byte[] rawWebP)
    {
        ImageSurface? bmp = null;
        var pinnedWebP = GCHandle.Alloc(rawWebP, GCHandleType.Pinned);
        var ptrData = pinnedWebP.AddrOfPinnedObject();

        try
        {
            //Get image width and height
            GetInfo(rawWebP, out var imgWidth, out var imgHeight, out var hasAlpha, out _, out _);

            if (!hasAlpha)
            {
                bmp = new ImageSurface(Format.RGB24, imgWidth, imgHeight);
                var outputSize = imgHeight * bmp.Stride;
                UnsafeNativeMethods.WebPDecodeBGRInto(ptrData, (nuint)rawWebP.Length, bmp.DataPtr, outputSize, bmp.Stride);
            }
            else
            {
                bmp = new ImageSurface(Format.ARGB32, imgWidth, imgHeight);
                var outputSize = imgHeight * bmp.Stride;
                UnsafeNativeMethods.WebPDecodeBGRAInto(ptrData, (nuint)rawWebP.Length, bmp.DataPtr, outputSize, bmp.Stride);
            }

            return bmp;
        }
        finally
        {
            bmp?.MarkDirty();

            //Free memory
            if (pinnedWebP.IsAllocated)
                pinnedWebP.Free();
        }
    }

    /// <summary>Decode a WebP image</summary>
    /// <param name="rawWebP">the data to uncompress</param>
    /// <param name="options">Options for advanced decode</param>
    /// <param name="pixelFormat"></param>
    /// <returns>Bitmap with the WebP image</returns>
    public static ImageSurface Decode(byte[] rawWebP, WebPDecoderOptions options, Format? pixelFormat = null)
    {
        ImageSurface? bmp = null;
        var pinnedWebP = GCHandle.Alloc(rawWebP, GCHandleType.Pinned);
        
        try
        {
            WebPDecoderConfig config = new();
            if (UnsafeNativeMethods.WebPInitDecoderConfig(ref config) == 0)
                throw new Exception("WebPInitDecoderConfig failed. Wrong version?");

            // Read the .webp input file information
            var ptrRawWebP = pinnedWebP.AddrOfPinnedObject();
            VP8StatusCode result;
            if (options.use_scaling == 0)
            {
                result = UnsafeNativeMethods.WebPGetFeatures(ptrRawWebP, (nuint)rawWebP.Length, ref config.input);
                if (result != VP8StatusCode.VP8_STATUS_OK)
                    throw new Exception("Failed WebPGetFeatures with error " + result);

                //Test cropping values
                if (options.use_cropping == 1)
                {
                    if (options.crop_left + options.crop_width > config.input.Width ||
                        options.crop_top + options.crop_height > config.input.Height)
                        throw new Exception("Crop options exceeded WebP image dimensions");
                }
            }
            else
            {
            }

            config.options.bypass_filtering = options.bypass_filtering;
            config.options.no_fancy_upsampling = options.no_fancy_upsampling;
            config.options.use_cropping = options.use_cropping;
            config.options.crop_left = options.crop_left;
            config.options.crop_top = options.crop_top;
            config.options.crop_width = options.crop_width;
            config.options.crop_height = options.crop_height;
            config.options.use_scaling = options.use_scaling;
            config.options.scaled_width = options.scaled_width;
            config.options.scaled_height = options.scaled_height;
            config.options.use_threads = options.use_threads;
            config.options.dithering_strength = options.dithering_strength;
            config.options.flip = options.flip;
            config.options.alpha_dithering_strength = options.alpha_dithering_strength;

            //Create a BitmapData and Lock all pixels to be written
            if (config.input.Has_alpha == 1)
            {
                config.output.colorspace = WEBP_CSP_MODE.MODE_bgrA;
                bmp = new ImageSurface(Format.ARGB32, config.input.Width, config.input.Height);
            }
            else
            {
                config.output.colorspace = WEBP_CSP_MODE.MODE_BGR;
                bmp = new ImageSurface(Format.RGB24, config.input.Width, config.input.Height);
            }

            // Specify the output format
            config.output.u.RGBA.rgba = bmp.DataPtr;
            config.output.u.RGBA.stride = bmp.Stride;
            config.output.u.RGBA.size = (nuint)(bmp.Height * bmp.Stride);
            config.output.height = bmp.Height;
            config.output.width = bmp.Width;
            config.output.is_external_memory = 1;

            // Decode
            result = UnsafeNativeMethods.WebPDecode(ptrRawWebP, (nuint)rawWebP.Length, ref config);
            if (result != VP8StatusCode.VP8_STATUS_OK)
            {
                throw new Exception("Failed WebPDecode with error " + result);
            }

            UnsafeNativeMethods.WebPFreeDecBuffer(ref config.output);

            return bmp;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message + "\r\nIn WebP.Decode");
        }
        finally
        {
            bmp?.MarkDirty();

            //Free memory
            if (pinnedWebP.IsAllocated)
                pinnedWebP.Free();
        }
    }

    #endregion

    #region | Public AnimDecoder Functions |

    /// <summary>Read and Decode an Animated WebP file</summary>
    /// <param name="pathFileName">Animated WebP file to load</param>
    /// <returns>Bitmaps of the Animated WebP frames</returns>
    public static IEnumerable<AnimatedFrame> AnimLoad(string pathFileName)
    {
        var rawWebP = File.ReadAllBytes(pathFileName);

        return AnimDecode(rawWebP);
    }

    /// <summary>Decode an Animated WebP image</summary>
    /// <param name="rawWebP">The data to uncompress</param>
    /// <returns>List of FrameData - each containing frame bitmap and duration</returns>
    public static IEnumerable<AnimatedFrame> AnimDecode(byte[] rawWebP)
    {
        var pinnedWebP = GCHandle.Alloc(rawWebP, GCHandleType.Pinned);

        try
        {
            var dec_options = new WebPAnimDecoderOptions();
            UnsafeNativeMethods.WebPAnimDecoderOptionsInit(ref dec_options);
            dec_options.color_mode = WEBP_CSP_MODE.MODE_BGRA;
            var webp_data = new WebPData
            {
                data = pinnedWebP.AddrOfPinnedObject(),
                size = (nuint)rawWebP.Length
            };
            var dec = UnsafeNativeMethods.WebPAnimDecoderNew(ref webp_data, ref dec_options);
            UnsafeNativeMethods.WebPAnimDecoderGetInfo(dec.decoder, out var anim_info);

            var frames = new List<AnimatedFrame>();
            var oldTimestamp = 0;
            while (UnsafeNativeMethods.WebPAnimDecoderHasMoreFrames(dec.decoder))
            {
                var buf = IntPtr.Zero;
                var timestamp = 0;
                UnsafeNativeMethods.WebPAnimDecoderGetNext(dec.decoder, ref buf, ref timestamp);

                var bitmap = new ImageSurface(Format.ARGB32, (int)anim_info.canvas_width, (int)anim_info.canvas_height);
                var startAddress = bitmap.DataPtr;
                var pixels = Math.Abs(bitmap.Stride) * bitmap.Height;
                unsafe
                {
                    var destPtr = startAddress.ToPointer();
                    var srcPtr = buf.ToPointer();
                    Buffer.MemoryCopy(srcPtr, destPtr, pixels, pixels);
                }
                bitmap.MarkDirty();

                frames.Add(new AnimatedFrame(bitmap, timestamp - oldTimestamp));
                oldTimestamp = timestamp;
            }

            UnsafeNativeMethods.WebPAnimDecoderDelete(dec.decoder);

            return frames;
        }
        finally
        {
            if (pinnedWebP.IsAllocated)
                pinnedWebP.Free();
        }
    }

    #endregion

    #region | Another Public Functions |

    /// <summary>Get the libwebp version</summary>
    /// <returns>Version of library</returns>
    public static string GetVersion()
    {
        try
        {
            var v = (uint)UnsafeNativeMethods.WebPGetDecoderVersion();
            var revision = v % 256;
            var minor = (v >> 8) % 256;
            var major = (v >> 16) % 256;
            return major + "." + minor + "." + revision;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message + "\r\nIn WebP.GetVersion");
        }
    }

    public static bool IsWebP(byte[] rawWebP)
    {
        return
            rawWebP.Length > 16 &&
            rawWebP[8] == 'W' &&
            rawWebP[9] == 'E' &&
            rawWebP[10] == 'B' &&
            rawWebP[11] == 'P';
    }

    /// <summary>Get info of WEBP data</summary>
    /// <param name="rawWebP">The data of WebP</param>
    /// <param name="width">width of image</param>
    /// <param name="height">height of image</param>
    /// <param name="has_alpha">Image has alpha channel</param>
    /// <param name="has_animation">Image is a animation</param>
    /// <param name="format">Format of image: 0 = undefined (/mixed), 1 = lossy, 2 = lossless</param>
    public static void GetInfo(byte[] rawWebP, out int width, out int height, out bool has_alpha, out bool has_animation,
        out string format)
    {
        var pinnedWebP = GCHandle.Alloc(rawWebP, GCHandleType.Pinned);

        try
        {
            var ptrRawWebP = pinnedWebP.AddrOfPinnedObject();
            WebPBitstreamFeatures features = new();
            var result = UnsafeNativeMethods.WebPGetFeatures(ptrRawWebP, (nuint)rawWebP.Length, ref features);

            if (result != 0)
                throw new Exception(result.ToString());

            width = features.Width;
            height = features.Height;
            has_alpha = features.Has_alpha == 1;
            has_animation = features.Has_animation == 1;
            format = features.Format switch
            {
                1 => "lossy",
                2 => "lossless",
                _ => "undefined",
            };
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message + "\r\nIn WebP.GetInfo");
        }
        finally
        {
            //Free memory
            if (pinnedWebP.IsAllocated)
                pinnedWebP.Free();
        }
    }

    #endregion
}

#region | Import libwebp functions |

[SuppressUnmanagedCodeSecurity]
internal static partial class UnsafeNativeMethods
{
    private const int WEBP_DECODER_ABI_VERSION = 0x0208;

    /// <summary>This function will initialize the configuration according to a predefined set of parameters (referred to by 'preset') and a given quality factor</summary>
    /// <param name="config">The WebPConfig structure</param>
    /// <param name="preset">Type of image</param>
    /// <param name="quality">Quality of compression</param>
    /// <param name="version"></param>
    [DllImport("libwebp.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "WebPConfigInitInternal")]
    internal static extern int WebPConfigInit(ref WebPConfig config, WebPPreset preset, float quality,
        int version = WEBP_DECODER_ABI_VERSION);

    /// <summary>Get info of WepP image</summary>
    /// <param name="rawWebP">Bytes[] of WebP image</param>
    /// <param name="data_size">Size of rawWebP</param>
    /// <param name="features">Features of WebP image</param>
    /// <param name="version"></param>
    /// <returns>VP8StatusCode</returns>
    [DllImport("libwebp.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "WebPGetFeaturesInternal")]
    internal static extern VP8StatusCode WebPGetFeatures([In()] nint rawWebP, nuint data_size,
        ref WebPBitstreamFeatures features, int version = WEBP_DECODER_ABI_VERSION);

    /// <summary>Activate the lossless compression mode with the desired efficiency</summary>
    /// <param name="config">The WebPConfig struct</param>
    /// <param name="level">between 0 (fastest, lowest compression) and 9 (slower, best compression)</param>
    /// <returns>0 in case of parameter error</returns>
    [DllImport("libwebp.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "WebPConfigLosslessPreset")]
    internal static extern int WebPConfigLosslessPreset(ref WebPConfig config, int level);

    /// <summary>Check that configuration is non-NULL and all configuration parameters are within their valid ranges</summary>
    /// <param name="config">The WebPConfig structure</param>
    /// <returns>1 if configuration is OK</returns>
    [DllImport("libwebp.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "WebPValidateConfig")]
    internal static extern int WebPValidateConfig(ref WebPConfig config);

    /// <summary>Initialize the WebPPicture structure checking the DLL version</summary>
    /// <param name="wpic">The WebPPicture structure</param>
    /// <param name="version"></param>
    [DllImport("libwebp.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "WebPPictureInitInternal")]
    internal static extern int WebPPictureInitInternal(ref WebPPicture wpic, int version = WEBP_DECODER_ABI_VERSION);

    /// <summary>Colorspace conversion function to import RGB samples</summary>
    /// <param name="wpic">The WebPPicture structure</param>
    /// <param name="bgr">Point to BGR data</param>
    /// <param name="stride">stride of BGR data</param>
    /// <returns>Returns 0 in case of memory error.</returns>
    [DllImport("libwebp.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "WebPPictureImportBGR")]
    internal static extern int WebPPictureImportBGR(ref WebPPicture wpic, nint bgr, int stride);

    /// <summary>Color-space conversion function to import RGB samples</summary>
    /// <param name="wpic">The WebPPicture structure</param>
    /// <param name="bgra">Point to BGRA data</param>
    /// <param name="stride">stride of BGRA data</param>
    /// <returns>Returns 0 in case of memory error.</returns>
    [DllImport("libwebp.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "WebPPictureImportBGRA")]
    internal static extern int WebPPictureImportBGRA(ref WebPPicture wpic, nint bgra, int stride);

    /// <summary>Color-space conversion function to import RGB samples</summary>
    /// <param name="wpic">The WebPPicture structure</param>
    /// <param name="bgr">Point to BGR data</param>
    /// <param name="stride">stride of BGR data</param>
    /// <returns>Returns 0 in case of memory error.</returns>
    [DllImport("libwebp.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "WebPPictureImportBGRX")]
    internal static extern int WebPPictureImportBGRX(ref WebPPicture wpic, nint bgr, int stride);

    /// <summary>The writer type for output compress data</summary>
    /// <param name="data">Data returned</param>
    /// <param name="data_size">Size of data returned</param>
    /// <param name="wpic">Picture structure</param>
    /// <returns></returns>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate int WebPMemoryWrite([In()] nint data, nuint data_size, ref WebPPicture wpic);

    /// <summary>Compress to WebP format</summary>
    /// <param name="config">The configuration structure for compression parameters</param>
    /// <param name="picture">'picture' hold the source samples in both YUV(A) or ARGB input</param>
    /// <returns>Returns 0 in case of error, 1 otherwise. In case of error, picture->error_code is updated accordingly.</returns>
    [DllImport("libwebp.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "WebPEncode")]
    internal static extern int WebPEncode(ref WebPConfig config, ref WebPPicture picture);

    /// <summary>Release the memory allocated by WebPPictureAlloc() or WebPPictureImport*()
    /// Note that this function does _not_ free the memory used by the 'picture' object itself.
    /// Besides memory (which is reclaimed) all other fields of 'picture' are preserved</summary>
    [DllImport("libwebp.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "WebPPictureFree")]
    internal static extern void WebPPictureFree(ref WebPPicture wpic);

    /// <summary>Validate the WebP image header and retrieve the image height and width. Pointers *width and *height can be passed NULL if deemed irrelevant</summary>
    /// <param name="data">Pointer to WebP image data</param>
    /// <param name="data_size">This is the size of the memory block pointed to by data containing the image data</param>
    /// <param name="width">The range is limited currently from 1 to 16383</param>
    /// <param name="height">The range is limited currently from 1 to 16383</param>
    /// <returns>1 if success, otherwise error code returned in the case of (a) formatting error(s).</returns>
    [DllImport("libwebp.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "WebPGetInfo")]
    internal static extern int WebPGetInfo([In()] nint data, nuint data_size, out int width, out int height);

    /// <summary>Decode WEBP image pointed to by *data and returns BGR samples into a preallocated buffer</summary>
    /// <param name="data">Pointer to WebP image data</param>
    /// <param name="data_size">This is the size of the memory block pointed to by data containing the image data</param>
    /// <param name="output_buffer">Pointer to decoded WebP image</param>
    /// <param name="output_buffer_size">Size of allocated buffer</param>
    /// <param name="output_stride">Specifies the distance between scan lines</param>
    [DllImport("libwebp.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "WebPDecodeBGRInto")]
    internal static extern nint WebPDecodeBGRInto([In()] nint data, nuint data_size, nint output_buffer,
        int output_buffer_size, int output_stride);

    /// <summary>Decode WEBP image pointed to by *data and returns BGRA samples into a preallocated buffer</summary>
    /// <param name="data">Pointer to WebP image data</param>
    /// <param name="data_size">This is the size of the memory block pointed to by data containing the image data</param>
    /// <param name="output_buffer">Pointer to decoded WebP image</param>
    /// <param name="output_buffer_size">Size of allocated buffer</param>
    /// <param name="output_stride">Specifies the distance between scan lines</param>
    [DllImport("libwebp.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "WebPDecodeBGRAInto")]
    internal static extern nint WebPDecodeBGRAInto([In()] nint data, nuint data_size, nint output_buffer,
        int output_buffer_size, int output_stride);

    /// <summary>Decode WEBP image pointed to by *data and returns ARGB samples into a preallocated buffer</summary>
    /// <param name="data">Pointer to WebP image data</param>
    /// <param name="data_size">This is the size of the memory block pointed to by data containing the image data</param>
    /// <param name="output_buffer">Pointer to decoded WebP image</param>
    /// <param name="output_buffer_size">Size of allocated buffer</param>
    /// <param name="output_stride">Specifies the distance between scan lines</param>
    [DllImport("libwebp.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "WebPDecodeARGBInto")]
    internal static extern nint WebPDecodeARGBInto([In()] nint data, nuint data_size, nint output_buffer,
        int output_buffer_size, int output_stride);

    /// <summary>Initialize the configuration as empty. This function must always be called first, unless WebPGetFeatures() is to be called</summary>
    /// <param name="webPDecoderConfig">Configuration structure</param>
    /// <param name="version"></param>
    /// <returns>False in case of mismatched version.</returns>
    [DllImport("libwebp.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "WebPInitDecoderConfigInternal")]
    internal static extern int WebPInitDecoderConfig(ref WebPDecoderConfig webPDecoderConfig,
        int version = WEBP_DECODER_ABI_VERSION);

    /// <summary>Decodes the full data at once, taking configuration into account</summary>
    /// <param name="data">WebP raw data to decode</param>
    /// <param name="data_size">Size of WebP data </param>
    /// <param name="config"></param>
    /// <returns>VP8_STATUS_OK if the decoding was successful</returns>
    [DllImport("libwebp.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "WebPDecode")]
    internal static extern VP8StatusCode WebPDecode(nint data, nuint data_size, ref WebPDecoderConfig config);

    /// <summary>Free any memory associated with the buffer. Must always be called last. Doesn't free the 'buffer' structure itself</summary>
    /// <param name="buffer">WebPDecBuffer</param>
    [DllImport("libwebp.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "WebPFreeDecBuffer")]
    internal static extern void WebPFreeDecBuffer(ref WebPDecBuffer buffer);

    /// <summary>Lossy encoding images</summary>
    /// <param name="bgr">Pointer to BGR image data</param>
    /// <param name="width">The range is limited currently from 1 to 16383</param>
    /// <param name="height">The range is limited currently from 1 to 16383</param>
    /// <param name="stride">Specifies the distance between scanlines</param>
    /// <param name="quality_factor">Ranges from 0 (lower quality) to 100 (highest quality). Controls the loss and quality during compression</param>
    /// <param name="output">output_buffer with WebP image</param>
    /// <returns>Size of WebP Image or 0 if an error occurred</returns>
    [DllImport("libwebp.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "WebPEncodeBGR")]
    internal static extern int WebPEncodeBGR([In()] nint bgr, int width, int height, int stride, float quality_factor,
        out nint output);

    /// <summary>Lossy encoding images</summary>
    /// <param name="bgra"></param>
    /// <param name="width">The range is limited currently from 1 to 16383</param>
    /// <param name="height">The range is limited currently from 1 to 16383</param>
    /// <param name="stride">Specifies the distance between scan lines</param>
    /// <param name="quality_factor">Ranges from 0 (lower quality) to 100 (highest quality). Controls the loss and quality during compression</param>
    /// <param name="output">output_buffer with WebP image</param>
    /// <returns>Size of WebP Image or 0 if an error occurred</returns>
    [DllImport("libwebp.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "WebPEncodeBGRA")]
    internal static extern int WebPEncodeBGRA([In()] nint bgra, int width, int height, int stride, float quality_factor,
        out nint output);

    /// <summary>Lossless encoding images pointed to by *data in WebP format</summary>
    /// <param name="bgr">Pointer to BGR image data</param>
    /// <param name="width">The range is limited currently from 1 to 16383</param>
    /// <param name="height">The range is limited currently from 1 to 16383</param>
    /// <param name="stride">Specifies the distance between scan lines</param>
    /// <param name="output">output_buffer with WebP image</param>
    /// <returns>Size of WebP Image or 0 if an error occurred</returns>
    [DllImport("libwebp.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "WebPEncodeLosslessBGR")]
    internal static extern int
        WebPEncodeLosslessBGR([In()] nint bgr, int width, int height, int stride, out nint output);

    /// <summary>Lossless encoding images pointed to by *data in WebP format</summary>
    /// <param name="bgra">Pointer to BGRA image data</param>
    /// <param name="width">The range is limited currently from 1 to 16383</param>
    /// <param name="height">The range is limited currently from 1 to 16383</param>
    /// <param name="stride">Specifies the distance between scan lines</param>
    /// <param name="output">output_buffer with WebP image</param>
    /// <returns>Size of WebP Image or 0 if an error occurred</returns>
    [DllImport("libwebp.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "WebPEncodeLosslessBGRA")]
    internal static extern int WebPEncodeLosslessBGRA([In()] nint bgra, int width, int height, int stride,
        out nint output);

    /// <summary>Releases memory returned by the WebPEncode</summary>
    /// <param name="p">Pointer to memory</param>
    [DllImport("libwebp.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "WebPFree")]
    internal static extern void WebPFree(nint p);

    /// <summary>Get the WebP version library</summary>
    /// <returns>8bits for each of major/minor/revision packet in integer. E.g: v2.5.7 is 0x020507</returns>
    [DllImport("libwebp.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "WebPGetDecoderVersion")]
    internal static extern int WebPGetDecoderVersion();

    /// <summary>Compute PSNR, SSIM or LSIM distortion metric between two pictures</summary>
    /// <param name="srcPicture">Picture to measure</param>
    /// <param name="refPicture">Reference picture</param>
    /// <param name="metric_type">0 = PSNR, 1 = SSIM, 2 = LSIM</param>
    /// <param name="pResult">dB in the Y/U/V/Alpha/All order</param>
    /// <returns>False in case of error (the two pictures don't have same dimension, ...)</returns>
    [DllImport("libwebp.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "WebPPictureDistortion")]
    internal static extern int WebPPictureDistortion(ref WebPPicture srcPicture, ref WebPPicture refPicture,
        int metric_type, nint pResult);

    [DllImport("libwebp.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "WebPMalloc")]
    internal static extern nint WebPMalloc(int size);
}

#endregion

#region | Import libwebpdemux functions |

[SuppressUnmanagedCodeSecurity]
internal static partial class UnsafeNativeMethods
{
    [MethodImpl(256)] //MethodImplOptions.AggressiveInlining
    private static void ValidatePlatform()
    {
        if ( /*nint.Size != 4 &&*/ IntPtr.Size != 8)
            throw new InvalidOperationException("Invalid platform. Can not find proper function");
    }

    /*
     * from WebPAnimDecoder API
     */

    private const int WEBP_DEMUX_ABI_VERSION = 0x0107;

    /// <summary>Should always be called, to initialize a fresh WebPAnimDecoderOptions
    /// structure before modification. Returns false in case of version mismatch.
    /// WebPAnimDecoderOptionsInit() must have succeeded before using the
    /// 'dec_options' object.</summary>
    /// <param name="dec_options">(in/out) options used for decoding animation</param>
    /// <returns>true/false - success/error</returns>
    internal static bool WebPAnimDecoderOptionsInit(ref WebPAnimDecoderOptions dec_options)
    {
        ValidatePlatform();

        return WebPAnimDecoderOptionsInitInternal(ref dec_options, WEBP_DEMUX_ABI_VERSION) == 1;
    }

    [DllImport("libwebpdemux.dll", CallingConvention = CallingConvention.Cdecl,
        EntryPoint = "WebPAnimDecoderOptionsInitInternal")]
    private static extern int WebPAnimDecoderOptionsInitInternal(ref WebPAnimDecoderOptions dec_options, int version);

    /// <summary>
    /// Creates and initializes a WebPAnimDecoder object.
    /// </summary>
    /// <param name="webp_data">(in) WebP bitstream. This should remain unchanged during the 
    ///	 lifetime of the output WebPAnimDecoder object.</param>
    /// <param name="dec_options">(in) decoding options. Can be passed NULL to choose 
    ///	 reasonable defaults (in particular, color mode MODE_RGBA 
    ///	 will be picked).</param>
    /// <returns>A pointer to the newly created WebPAnimDecoder object, or NULL in case of
    ///	 parsing error, invalid option or memory error.</returns>
    internal static WebPAnimDecoder WebPAnimDecoderNew(ref WebPData webp_data, ref WebPAnimDecoderOptions dec_options)
    {
        var ptr = WebPAnimDecoderNewInternal(ref webp_data, ref dec_options, WEBP_DEMUX_ABI_VERSION);
        var decoder = new WebPAnimDecoder() { decoder = ptr };
        return decoder;
    }

    [DllImport("libwebpdemux.dll", CallingConvention = CallingConvention.Cdecl,
        EntryPoint = "WebPAnimDecoderNewInternal")]
    private static extern nint WebPAnimDecoderNewInternal(ref WebPData webp_data, ref WebPAnimDecoderOptions dec_options,
        int version);

    /// <summary>Get global information about the animation.</summary>
    /// <param name="dec">(in) decoder instance to get information from.</param>
    /// <param name="info">(out) global information fetched from the animation.</param>
    /// <returns>True on success.</returns>
    internal static bool WebPAnimDecoderGetInfo(nint dec, out WebPAnimInfo info)
    {
        return WebPAnimDecoderGetInfoInternal(dec, out info) == 1;
    }

    [DllImport("libwebpdemux.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "WebPAnimDecoderGetInfo")]
    private static extern int WebPAnimDecoderGetInfoInternal(nint dec, out WebPAnimInfo info);

    /// <summary>Check if there are more frames left to decode.</summary>
    /// <param name="dec">(in) decoder instance to be checked.</param>
    /// <returns>
    /// True if 'dec' is not NULL and some frames are yet to be decoded.
    /// Otherwise, returns false.
    /// </returns>
    internal static bool WebPAnimDecoderHasMoreFrames(nint dec)
    {
        return WebPAnimDecoderHasMoreFramesInternal(dec) == 1;
    }

    [DllImport("libwebpdemux.dll", CallingConvention = CallingConvention.Cdecl,
        EntryPoint = "WebPAnimDecoderHasMoreFrames")]
    private static extern int WebPAnimDecoderHasMoreFramesInternal(nint dec);

    /// <summary>
    /// Fetch the next frame from 'dec' based on options supplied to
    /// WebPAnimDecoderNew(). This will be a fully reconstructed canvas of size
    /// 'canvas_width * 4 * canvas_height', and not just the frame sub-rectangle. The
    /// returned buffer 'buf' is valid only until the next call to
    /// WebPAnimDecoderGetNext(), WebPAnimDecoderReset() or WebPAnimDecoderDelete().
    /// </summary>
    /// <param name="dec">(in/out) decoder instance from which the next frame is to be fetched.</param>
    /// <param name="buf">(out) decoded frame.</param>
    /// <param name="timestamp">(out) timestamp of the frame in milliseconds.</param>
    /// <returns>
    /// False if any of the arguments are NULL, or if there is a parsing or
    /// decoding error, or if there are no more frames. Otherwise, returns true.
    /// </returns>
    internal static bool WebPAnimDecoderGetNext(nint dec, ref nint buf, ref int timestamp)
    {
        return WebPAnimDecoderGetNextInternal(dec, ref buf, ref timestamp) == 1;
    }

    [DllImport("libwebpdemux.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "WebPAnimDecoderGetNext")]
    private static extern int WebPAnimDecoderGetNextInternal(nint dec, ref nint buf, ref int timestamp);

    /// <summary>
    /// Resets the WebPAnimDecoder object, so that next call to
    /// WebPAnimDecoderGetNext() will restart decoding from 1st frame. This would be
    /// helpful when all frames need to be decoded multiple times (e.g.
    /// info.loop_count times) without destroying and recreating the 'dec' object.
    /// </summary>
    /// <param name="dec">(in/out) decoder instance to be reset</param>
    internal static void WebPAnimDecoderReset(nint dec)
    {
        WebPAnimDecoderResetInternal(dec);
    }

    [DllImport("libwebpdemux.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "WebPAnimDecoderReset")]
    private static extern void WebPAnimDecoderResetInternal(nint dec);

    /// <summary>Deletes the WebPAnimDecoder object.</summary>
    /// <param name="decoder">(in/out) decoder instance to be deleted</param>
    internal static void WebPAnimDecoderDelete(nint decoder)
    {
        WebPAnimDecoderDeleteInternal(decoder);
    }

    [DllImport("libwebpdemux.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "WebPAnimDecoderDelete")]
    private static extern void WebPAnimDecoderDeleteInternal(nint dec);
}

#endregion

#region | Predefined |

/// <summary>Enumerate some predefined settings for WebPConfig, depending on the type of source picture. These presets are used when calling WebPConfigPreset()</summary>
internal enum WebPPreset
{
    /// <summary>Default preset</summary>
    WEBP_PRESET_DEFAULT = 0,

    /// <summary>Digital picture, like portrait, inner shot</summary>
    WEBP_PRESET_PICTURE,

    /// <summary>Outdoor photograph, with natural lighting</summary>
    WEBP_PRESET_PHOTO,

    /// <summary>Hand or line drawing, with high-contrast details</summary>
    WEBP_PRESET_DRAWING,

    /// <summary>Small-sized colorful images</summary>
    WEBP_PRESET_ICON,

    /// <summary>Text-like</summary>
    WEBP_PRESET_TEXT
};

/// <summary>Encoding error conditions</summary>
internal enum WebPEncodingError
{
    /// <summary>No error</summary>
    VP8_ENC_OK = 0,

    /// <summary>Memory error allocating objects</summary>
    VP8_ENC_ERROR_OUT_OF_MEMORY,

    /// <summary>Memory error while flushing bits</summary>
    VP8_ENC_ERROR_BITSTREAM_OUT_OF_MEMORY,

    /// <summary>A pointer parameter is NULL</summary>
    VP8_ENC_ERROR_NULL_PARAMETER,

    /// <summary>Configuration is invalid</summary>
    VP8_ENC_ERROR_INVALID_CONFIGURATION,

    /// <summary>Picture has invalid width/height</summary>
    VP8_ENC_ERROR_BAD_DIMENSION,

    /// <summary>Partition is bigger than 512k</summary>
    VP8_ENC_ERROR_PARTITION0_OVERFLOW,

    /// <summary>Partition is bigger than 16M</summary>
    VP8_ENC_ERROR_PARTITION_OVERFLOW,

    /// <summary>Error while flushing bytes</summary>
    VP8_ENC_ERROR_BAD_WRITE,

    /// <summary>File is bigger than 4G</summary>
    VP8_ENC_ERROR_FILE_TOO_BIG,

    /// <summary>Abort request by user</summary>
    VP8_ENC_ERROR_USER_ABORT,

    /// <summary>List terminator. Always last</summary>
    VP8_ENC_ERROR_LAST,
}

/// <summary>Enumeration of the status codes</summary>
internal enum VP8StatusCode
{
    /// <summary>No error</summary>
    VP8_STATUS_OK = 0,

    /// <summary>Memory error allocating objects</summary>
    VP8_STATUS_OUT_OF_MEMORY,

    /// <summary>Configuration is invalid</summary>
    VP8_STATUS_INVALID_PARAM,
    VP8_STATUS_BITSTREAM_ERROR,

    /// <summary>Configuration is invalid</summary>
    VP8_STATUS_UNSUPPORTED_FEATURE,
    VP8_STATUS_SUSPENDED,

    /// <summary>Abort request by user</summary>
    VP8_STATUS_USER_ABORT,
    VP8_STATUS_NOT_ENOUGH_DATA,
}

/// <summary>Image characteristics hint for the underlying encoder</summary>
internal enum WebPImageHint
{
    /// <summary>Default preset</summary>
    WEBP_HINT_DEFAULT = 0,

    /// <summary>Digital picture, like portrait, inner shot</summary>
    WEBP_HINT_PICTURE,

    /// <summary>Outdoor photograph, with natural lighting</summary>
    WEBP_HINT_PHOTO,

    /// <summary>Discrete tone image (graph, map-tile etc)</summary>
    WEBP_HINT_GRAPH,

    /// <summary>List terminator. Always last</summary>
    WEBP_HINT_LAST
};

/// <summary>Describes the byte-ordering of packed samples in memory</summary>
public enum WEBP_CSP_MODE
{
    /// <summary>Byte-order: R,G,B,R,G,B,..</summary>
    MODE_RGB = 0,

    /// <summary>Byte-order: R,G,B,A,R,G,B,A,..</summary>
    MODE_RGBA = 1,

    /// <summary>Byte-order: B,G,R,B,G,R,..</summary>
    MODE_BGR = 2,

    /// <summary>Byte-order: B,G,R,A,B,G,R,A,..</summary>
    MODE_BGRA = 3,

    /// <summary>Byte-order: A,R,G,B,A,R,G,B,..</summary>
    MODE_ARGB = 4,

    /// <summary>Byte-order: RGB-565: [a4 a3 a2 a1 a0 r5 r4 r3], [r2 r1 r0 g4 g3 g2 g1 g0], ...
    /// WEBP_SWAP_16BITS_CSP is defined, 
    /// Byte-order: RGB-565: [a4 a3 a2 a1 a0 b5 b4 b3], [b2 b1 b0 g4 g3 g2 g1 g0], ..</summary>
    MODE_RGBA_4444 = 5,

    /// <summary>Byte-order: RGB-565: [r4 r3 r2 r1 r0 g5 g4 g3], [g2 g1 g0 b4 b3 b2 b1 b0], ...
    /// WEBP_SWAP_16BITS_CSP is defined, 
    /// Byte-order: [b3 b2 b1 b0 a3 a2 a1 a0], [r3 r2 r1 r0 g3 g2 g1 g0], ..</summary>
    MODE_RGB_565 = 6,

    /// <summary>RGB-premultiplied transparent modes (alpha value is preserved)</summary>
    MODE_rgbA = 7,

    /// <summary>RGB-premultiplied transparent modes (alpha value is preserved)</summary>
    MODE_bgrA = 8,

    /// <summary>RGB-premultiplied transparent modes (alpha value is preserved)</summary>
    MODE_Argb = 9,

    /// <summary>RGB-premultiplied transparent modes (alpha value is preserved)</summary>
    MODE_rgbA_4444 = 10,

    /// <summary>YUV 4:2:0</summary>
    MODE_YUV = 11,

    /// <summary>YUV 4:2:0</summary>
    MODE_YUVA = 12,

    /// <summary>MODE_LAST -> 13</summary>
    MODE_LAST = 13,
}

/// <summary>
/// Decoding states. State normally flows as:
/// WEBP_HEADER->VP8_HEADER->VP8_PARTS0->VP8_DATA->DONE for a lossy image, and
/// WEBP_HEADER->VP8L_HEADER->VP8L_DATA->DONE for a lossless image.
/// If there is any error the decoder goes into state ERROR.
/// </summary>
internal enum DecState
{
    STATE_WEBP_HEADER, // All the data before that of the VP8/VP8L chunk.
    STATE_VP8_HEADER, // The VP8 Frame header (within the VP8 chunk).
    STATE_VP8_PARTS0,
    STATE_VP8_DATA,
    STATE_VP8L_HEADER,
    STATE_VP8L_DATA,
    STATE_DONE,
    STATE_ERROR
};

#endregion

#region | libwebp structs |

/// <summary>Features gathered from the bit stream</summary>
[StructLayout(LayoutKind.Sequential)]
internal struct WebPBitstreamFeatures
{
    /// <summary>Width in pixels, as read from the bit stream</summary>
    public int Width;

    /// <summary>Height in pixels, as read from the bit stream</summary>
    public int Height;

    /// <summary>True if the bit stream contains an alpha channel</summary>
    public int Has_alpha;

    /// <summary>True if the bit stream is an animation</summary>
    public int Has_animation;

    /// <summary>0 = undefined (/mixed), 1 = lossy, 2 = lossless</summary>
    public int Format;

    /// <summary>Padding for later use</summary>
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5, ArraySubType = UnmanagedType.U4)]
    private readonly uint[] pad;
};

/// <summary>Compression parameters</summary>
[StructLayout(LayoutKind.Sequential)]
internal struct WebPConfig
{
    /// <summary>Lossless encoding (0=lossy(default), 1=lossless)</summary>
    public int lossless;

    /// <summary>Between 0 (smallest file) and 100 (biggest)</summary>
    public float quality;

    /// <summary>Quality/speed trade-off (0=fast, 6=slower-better)</summary>
    public int method;

    /// <summary>Hint for image type (lossless only for now)</summary>
    public WebPImageHint image_hint;

    /// <summary>If non-zero, set the desired target size in bytes. Takes precedence over the 'compression' parameter</summary>
    public int target_size;

    /// <summary>If non-zero, specifies the minimal distortion to try to achieve. Takes precedence over target_size</summary>
    public float target_PSNR;

    /// <summary>Maximum number of segments to use, in [1..4]</summary>
    public int segments;

    /// <summary>Spatial Noise Shaping. 0=off, 100=maximum</summary>
    public int sns_strength;

    /// <summary>Range: [0 = off .. 100 = strongest]</summary>
    public int filter_strength;

    /// <summary>Range: [0 = off .. 7 = least sharp]</summary>
    public int filter_sharpness;

    /// <summary>Filtering type: 0 = simple, 1 = strong (only used if filter_strength > 0 or auto-filter > 0)</summary>
    public int filter_type;

    /// <summary>Auto adjust filter's strength [0 = off, 1 = on]</summary>
    public int autofilter;

    /// <summary>Algorithm for encoding the alpha plane (0 = none, 1 = compressed with WebP lossless). Default is 1</summary>
    public int alpha_compression;

    /// <summary>Predictive filtering method for alpha plane. 0: none, 1: fast, 2: best. Default if 1</summary>
    public int alpha_filtering;

    /// <summary>Between 0 (smallest size) and 100 (lossless). Default is 100</summary>
    public int alpha_quality;

    /// <summary>Number of entropy-analysis passes (in [1..10])</summary>
    public int pass;

    /// <summary>If true, export the compressed picture back. In-loop filtering is not applied</summary>
    public int show_compressed;

    /// <summary>Preprocessing filter (0=none, 1=segment-smooth, 2=pseudo-random dithering)</summary>
    public int preprocessing;

    /// <summary>Log2(number of token partitions) in [0..3] Default is set to 0 for easier progressive decoding</summary>
    public int partitions;

    /// <summary>Quality degradation allowed to fit the 512k limit on prediction modes coding (0: no degradation, 100: maximum possible degradation)</summary>
    public int partition_limit;

    /// <summary>If true, compression parameters will be remapped to better match the expected output size from JPEG compression. Generally, the output size will be similar but the degradation will be lower</summary>
    public int emulate_jpeg_size;

    /// <summary>If non-zero, try and use multi-threaded encoding</summary>
    public int thread_level;

    /// <summary>If set, reduce memory usage (but increase CPU use)</summary>
    public int low_memory;

    /// <summary>Near lossless encoding [0 = max loss .. 100 = off (default)]</summary>
    public int near_lossless;

    /// <summary>If non-zero, preserve the exact RGB values under transparent area. Otherwise, discard this invisible RGB information for better compression. The default value is 0</summary>
    public int exact;

    /// <summary>Reserved for future lossless feature</summary>
    public int delta_palettization;

    /// <summary>If needed, use sharp (and slow) RGB->YUV conversion</summary>
    public int use_sharp_yuv;

    /// <summary>Padding for later use</summary>
    private readonly int pad1;

    private readonly int pad2;
};

/// <summary>Main exchange structure (input samples, output bytes, statistics)</summary>
[StructLayout(LayoutKind.Sequential)]
internal struct WebPPicture
{
    /// <summary>Main flag for encoder selecting between ARGB or YUV input. Recommended to use ARGB input (*argb, argb_stride) for lossless, and YUV input (*y, *u, *v, etc.) for lossy</summary>
    public int use_argb;

    /// <summary>Color-space: should be YUV420 for now (=Y'CbCr). Value = 0</summary>
    public uint colorspace;

    /// <summary>Width of picture (less or equal to WEBP_MAX_DIMENSION)</summary>
    public int width;

    /// <summary>Height of picture (less or equal to WEBP_MAX_DIMENSION)</summary>
    public int height;

    /// <summary>Pointer to luma plane</summary>
    public nint y;

    /// <summary>Pointer to chroma U plane</summary>
    public nint u;

    /// <summary>Pointer to chroma V plane</summary>
    public nint v;

    /// <summary>Luma stride</summary>
    public int y_stride;

    /// <summary>Chroma stride</summary>
    public int uv_stride;

    /// <summary>Pointer to the alpha plane</summary>
    public nint a;

    /// <summary>stride of the alpha plane</summary>
    public int a_stride;

    /// <summary>Padding for later use</summary>
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2, ArraySubType = UnmanagedType.U4)]
    private readonly uint[] pad1;

    /// <summary>Pointer to ARGB (32 bit) plane</summary>
    public nint argb;

    /// <summary>This is stride in pixels units, not bytes</summary>
    public int argb_stride;

    /// <summary>Padding for later use</summary>
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = UnmanagedType.U4)]
    private readonly uint[] pad2;

    /// <summary>Byte-emission hook, to store compressed bytes as they are ready</summary>
    public nint writer;

    /// <summary>Can be used by the writer</summary>
    public nint custom_ptr;

    // map for extra information (only for lossy compression mode)
    /// <summary>1: intra type, 2: segment, 3: quant, 4: intra-16 prediction mode, 5: chroma prediction mode, 6: bit cost, 7: distortion</summary>
    public int extra_info_type;

    /// <summary>If not NULL, points to an array of size ((width + 15) / 16) * ((height + 15) / 16) that will be filled with a macroblock map, depending on extra_info_type</summary>
    public nint extra_info;

    /// <summary>Pointer to side statistics (updated only if not NULL)</summary>
    public nint stats;

    /// <summary>Error code for the latest error encountered during encoding</summary>
    public uint error_code;

    /// <summary>If not NULL, report progress during encoding</summary>
    public nint progress_hook;

    /// <summary>This field is free to be set to any value and used during callbacks (like progress-report e.g.)</summary>
    public nint user_data;

    /// <summary>Padding for later use</summary>
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 13, ArraySubType = UnmanagedType.U4)]
    private readonly uint[] pad3;

    /// <summary>Row chunk of memory for YUVA planes</summary>
    private readonly nint memory_;

    /// <summary>Row chunk of memory for ARGB planes</summary>
    private readonly nint memory_argb_;

    /// <summary>Padding for later use</summary>
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2, ArraySubType = UnmanagedType.U4)]
    private readonly uint[] pad4;
};

/// <summary>Structure for storing auxiliary statistics (mostly for lossy encoding)</summary>
[StructLayout(LayoutKind.Sequential)]
internal struct WebPAuxStats
{
    /// <summary>Final size</summary>
    public int coded_size;

    /// <summary>Peak-signal-to-noise ratio for Y</summary>
    public float PSNRY;

    /// <summary>Peak-signal-to-noise ratio for U</summary>
    public float PSNRU;

    /// <summary>Peak-signal-to-noise ratio for V</summary>
    public float PSNRV;

    /// <summary>Peak-signal-to-noise ratio for All</summary>
    public float PSNRALL;

    /// <summary>Peak-signal-to-noise ratio for Alpha</summary>
    public float PSNRAlpha;

    /// <summary>Number of intra4</summary>
    public int block_count_intra4;

    /// <summary>Number of intra16</summary>
    public int block_count_intra16;

    /// <summary>Number of skipped macro-blocks</summary>
    public int block_count_skipped;

    /// <summary>Approximate number of bytes spent for header</summary>
    public int header_bytes;

    /// <summary>Approximate number of bytes spent for  mode-partition #0</summary>
    public int mode_partition_0;

    /// <summary>Approximate number of bytes spent for DC coefficients for segment 0</summary>
    public int residual_bytes_DC_segments0;

    /// <summary>Approximate number of bytes spent for AC coefficients for segment 0</summary>
    public int residual_bytes_AC_segments0;

    /// <summary>Approximate number of bytes spent for UV coefficients for segment 0</summary>
    public int residual_bytes_uv_segments0;

    /// <summary>Approximate number of bytes spent for DC coefficients for segment 1</summary>
    public int residual_bytes_DC_segments1;

    /// <summary>Approximate number of bytes spent for AC coefficients for segment 1</summary>
    public int residual_bytes_AC_segments1;

    /// <summary>Approximate number of bytes spent for UV coefficients for segment 1</summary>
    public int residual_bytes_uv_segments1;

    /// <summary>Approximate number of bytes spent for DC coefficients for segment 2</summary>
    public int residual_bytes_DC_segments2;

    /// <summary>Approximate number of bytes spent for AC coefficients for segment 2</summary>
    public int residual_bytes_AC_segments2;

    /// <summary>Approximate number of bytes spent for UV coefficients for segment 2</summary>
    public int residual_bytes_uv_segments2;

    /// <summary>Approximate number of bytes spent for DC coefficients for segment 3</summary>
    public int residual_bytes_DC_segments3;

    /// <summary>Approximate number of bytes spent for AC coefficients for segment 3</summary>
    public int residual_bytes_AC_segments3;

    /// <summary>Approximate number of bytes spent for UV coefficients for segment 3</summary>
    public int residual_bytes_uv_segments3;

    /// <summary>Number of macro-blocks in segments 0</summary>
    public int segment_size_segments0;

    /// <summary>Number of macro-blocks in segments 1</summary>
    public int segment_size_segments1;

    /// <summary>Number of macro-blocks in segments 2</summary>
    public int segment_size_segments2;

    /// <summary>Number of macro-blocks in segments 3</summary>
    public int segment_size_segments3;

    /// <summary>Quantizer values for segment 0</summary>
    public int segment_quant_segments0;

    /// <summary>Quantizer values for segment 1</summary>
    public int segment_quant_segments1;

    /// <summary>Quantizer values for segment 2</summary>
    public int segment_quant_segments2;

    /// <summary>Quantizer values for segment 3</summary>
    public int segment_quant_segments3;

    /// <summary>Filtering strength for segment 0 [0..63]</summary>
    public int segment_level_segments0;

    /// <summary>Filtering strength for segment 1 [0..63]</summary>
    public int segment_level_segments1;

    /// <summary>Filtering strength for segment 2 [0..63]</summary>
    public int segment_level_segments2;

    /// <summary>Filtering strength for segment 3 [0..63]</summary>
    public int segment_level_segments3;

    /// <summary>Size of the transparency data</summary>
    public int alpha_data_size;

    /// <summary>Size of the enhancement layer data</summary>
    public int layer_data_size;

    // lossless encoder statistics
    /// <summary>bit0:predictor bit1:cross-color transform bit2:subtract-green bit3:color indexing</summary>
    public int lossless_features;

    /// <summary>Number of precision bits of histogram</summary>
    public int histogram_bits;

    /// <summary>Precision bits for transform</summary>
    public int transform_bits;

    /// <summary>Number of bits for color cache lookup</summary>
    public int cache_bits;

    /// <summary>Number of color in palette, if used</summary>
    public int palette_size;

    /// <summary>Final lossless size</summary>
    public int lossless_size;

    /// <summary>Lossless header (transform, Huffman, etc) size</summary>
    public int lossless_hdr_size;

    /// <summary>Lossless image data size</summary>
    public int lossless_data_size;

    /// <summary>Padding for later use</summary>
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2, ArraySubType = UnmanagedType.U4)]
    private readonly uint[] pad;
};

[StructLayout(LayoutKind.Sequential)]
internal struct WebPDecoderConfig
{
    /// <summary>Immutable bit stream features (optional)</summary>
    public WebPBitstreamFeatures input;

    /// <summary>Output buffer (can point to external memory)</summary>
    public WebPDecBuffer output;

    /// <summary>Decoding options</summary>
    public WebPDecoderOptions options;
}

/// <summary>Output buffer</summary>
[StructLayout(LayoutKind.Sequential)]
internal struct WebPDecBuffer
{
    /// <summary>Color space</summary>
    public WEBP_CSP_MODE colorspace;

    /// <summary>Width of image</summary>
    public int width;

    /// <summary>Height of image</summary>
    public int height;

    /// <summary>If non-zero, 'internal_memory' pointer is not used. If value is '2' or more, the external memory is considered 'slow' and multiple read/write will be avoided</summary>
    public int is_external_memory;

    /// <summary>Output buffer parameters</summary>
    public RGBA_YUVA_Buffer u;

    /// <summary>Padding for later use</summary>
    private readonly uint pad1;

    /// <summary>Padding for later use</summary>
    private readonly uint pad2;

    /// <summary>Padding for later use</summary>
    private readonly uint pad3;

    /// <summary>Padding for later use</summary>
    private readonly uint pad4;

    /// <summary>Internally allocated memory (only when is_external_memory is 0). Should not be used externally, but accessed via WebPRGBABuffer</summary>
    public nint private_memory;
}

/// <summary>Union of buffer parameters</summary>
[StructLayout(LayoutKind.Explicit)]
internal struct RGBA_YUVA_Buffer
{
    [FieldOffset(0)] public WebPRGBABuffer RGBA;

    [FieldOffset(0)] public WebPYUVABuffer YUVA;
}

[StructLayout(LayoutKind.Sequential)]
internal struct WebPYUVABuffer
{
    /// <summary>Pointer to luma samples</summary>
    public nint y;

    /// <summary>Pointer to chroma U samples</summary>
    public nint u;

    /// <summary>Pointer to chroma V samples</summary>
    public nint v;

    /// <summary>Pointer to alpha samples</summary>
    public nint a;

    /// <summary>Luma stride</summary>
    public int y_stride;

    /// <summary>Chroma U stride</summary>
    public int u_stride;

    /// <summary>Chroma V stride</summary>
    public int v_stride;

    /// <summary>Alpha stride</summary>
    public int a_stride;

    /// <summary>Luma plane size</summary>
    public nuint y_size;

    /// <summary>Chroma plane U size</summary>
    public nuint u_size;

    /// <summary>Chroma plane V size</summary>
    public nuint v_size;

    /// <summary>Alpha plane size</summary>
    public nuint a_size;
}

/// <summary>Generic structure for describing the output sample buffer</summary>
[StructLayout(LayoutKind.Sequential)]
internal struct WebPRGBABuffer
{
    /// <summary>Pointer to RGBA samples</summary>
    public nint rgba;

    /// <summary>Stride in bytes from one scanline to the next</summary>
    public int stride;

    /// <summary>Total size of the RGBA buffer</summary>
    public nuint size;
}

/// <summary>Decoding options</summary>
[StructLayout(LayoutKind.Sequential)]
public struct WebPDecoderOptions
{
    /// <summary>If true, skip the in-loop filtering</summary>
    public int bypass_filtering;

    /// <summary>If true, use faster point-wise up-sampler</summary>
    public int no_fancy_upsampling;

    /// <summary>If true, cropping is applied _first_</summary>
    public int use_cropping;

    /// <summary>Left position for cropping. Will be snapped to even values</summary>
    public int crop_left;

    /// <summary>Top position for cropping. Will be snapped to even values</summary>
    public int crop_top;

    /// <summary>Width of the cropping area</summary>
    public int crop_width;

    /// <summary>Height of the cropping area</summary>
    public int crop_height;

    /// <summary>If true, scaling is applied _afterward_</summary>
    public int use_scaling;

    /// <summary>Final width</summary>
    public int scaled_width;

    /// <summary>Final height</summary>
    public int scaled_height;

    /// <summary>If true, use multi-threaded decoding</summary>
    public int use_threads;

    /// <summary>Dithering strength (0=Off, 100=full)</summary>
    public int dithering_strength;

    /// <summary>Flip output vertically</summary>
    public int flip;

    /// <summary>Alpha dithering strength in [0..100]</summary>
    public int alpha_dithering_strength;

    /// <summary>Padding for later use</summary>
    private readonly uint pad1;

    /// <summary>Padding for later use</summary>
    private readonly uint pad2;

    /// <summary>Padding for later use</summary>
    private readonly uint pad3;

    /// <summary>Padding for later use</summary>
    private readonly uint pad4;

    /// <summary>Padding for later use</summary>
    private readonly uint pad5;
};

/*
 * from WebPAnimDecoder API
 */

/// <summary>Anim decoder options</summary>
[StructLayout(LayoutKind.Sequential)]
public struct WebPAnimDecoderOptions
{
    /// <summary>Output colorspace. Only the following modes are supported:
    /// MODE_RGBA, MODE_BGRA, MODE_rgbA and MODE_bgrA.</summary>
    public WEBP_CSP_MODE color_mode;

    /// <summary>If true, use multi-threaded decoding</summary>
    public int use_threads;

    /// <summary>Padding for later use</summary>
    private readonly uint pad1;

    /// <summary>Padding for later use</summary>
    private readonly uint pad2;

    /// <summary>Padding for later use</summary>
    private readonly uint pad3;

    /// <summary>Padding for later use</summary>
    private readonly uint pad4;

    /// <summary>Padding for later use</summary>
    private readonly uint pad5;

    /// <summary>Padding for later use</summary>
    private readonly uint pad6;

    /// <summary>Padding for later use</summary>
    private readonly uint pad7;
};

/// <summary>
/// Data type used to describe 'raw' data, e.g., chunk data
/// (ICC profile, metadata) and WebP compressed image data.
/// 'bytes' memory must be allocated using WebPMalloc() and such.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public struct WebPData
{
    public nint data;
    public nuint size;
}

/// <summary>Main opaque object.</summary>
[StructLayout(LayoutKind.Sequential)]
public struct WebPAnimDecoder
{
    public nint decoder;
}

/// <summary>Global information about the animation</summary>
[StructLayout(LayoutKind.Sequential)]
public struct WebPAnimInfo
{
    public uint canvas_width;
    public uint canvas_height;
    public uint loop_count;
    public uint bgcolor;
    public uint frame_count;

    /// <summary>Padding for later use</summary>
    private readonly uint pad1;

    /// <summary>Padding for later use</summary>
    private readonly uint pad2;

    /// <summary>Padding for later use</summary>
    private readonly uint pad3;

    /// <summary>Padding for later use</summary>
    private readonly uint pad4;
}

#endregion
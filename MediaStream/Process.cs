using FFMpegCore;
using FFMpegCore.Enums;

namespace MediaStream
{
    public static class Process
    {
        public static async Task UploadVideo(string filepath)
        {
            GlobalFFOptions.Configure(options => options.BinaryFolder = "./bin");
            _ = await FFMpegArguments
                .FromFileInput(filepath)
                .OutputToFile(filepath.Replace("_raw", "_720"), false, options => options
                    .WithVideoCodec(VideoCodec.LibX264)
                    .WithVideoBitrate(4096)
                    .WithAudioCodec(AudioCodec.Aac)
                    .WithVariableBitrate(4)
                    .WithVideoFilters(filterOptions => filterOptions
                        .Scale(VideoSize.Hd))
                    .WithFastStart())
                .ProcessAsynchronously();
        }
    }
}

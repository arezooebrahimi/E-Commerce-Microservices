using Common.Entities.FileManager;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Formats.Webp;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Formats.Png;
using FileManager.Services.Abstract;


namespace FileManager.Services.Concrete
{
    public class ImageProcessingService: IImageProcessingService
    {
        private readonly List<(string Name, int Width, int Height)> _formats = new()
        {
            ("thumbnail", 150, 150),
            ("small", 320, 0),
            ("medium", 640, 0),
            ("large", 1024, 0),
        };

        public async Task<List<MediaFormat>> GenerateFormatsAsync(Stream imageStream,string fileName, string outputFolderPath, string ext=".webp")
        {
            imageStream.Position = 0;

            using var image = await Image.LoadAsync(imageStream);

            var formats = new List<MediaFormat>();

            foreach (var format in _formats)
            {
                var resized = image.Clone(ctx => ctx.Resize(format.Width, format.Height));
                var outputPath = Path.Combine(outputFolderPath, $"{format.Name}-{fileName}{ext}");

                await resized.SaveAsync(outputPath, new WebpEncoder());

                formats.Add(new MediaFormat
                {
                    FileName = $"{format.Name}-{fileName}{ext}",
                    FilePath = outputPath,
                    Format = format.Name,
                    Ext = ext,
                    Width = resized.Width,
                    Height = resized.Height
                });
            }

            return formats;
        }

        public async Task<(string fileName, string filePath)> ConvertFormatAsync(Stream imageStream, string fileNameWithoutExt, string outputFolderPath, string ext = "webp")
        {
            imageStream.Position = 0;

            using var image = await Image.LoadAsync(imageStream);
            IImageEncoder encoder = ext.ToLower() switch
            {
                "webp" => new WebpEncoder(),
                "jpeg" or "jpg" => new JpegEncoder(),
                "png" => new PngEncoder(),
                _ => new WebpEncoder(),
            };

            var filePath = Path.Combine(outputFolderPath, $"{fileNameWithoutExt}{ext}");
            await image.SaveAsync(filePath, encoder);

            return ($"{fileNameWithoutExt}{ext}", filePath);
        }
    }
}

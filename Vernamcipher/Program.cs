using System.Drawing;

//string key = "ABC";
//string message = "HELLO";
//string encrytped = "";

//for(int i = 0; i < message.Length; i++)
//{
//    encrytped += (char)key[i];
//}
//Console.WriteLine(encrytped);

const string keyPath = "C:\\Users\\Leon\\source\\repos\\Vernamcipher\\Vernamcipher\\key.png";

const string imagePath = "C:\\Users\\Leon\\source\\repos\\Vernamcipher\\Vernamcipher\\rocket.png";
static string LoadFile(string path)
{
    string text = "";
    try
    {
        text = File.ReadAllText(path);
    }
    catch (System.IO.FileNotFoundException)
    {
        Console.WriteLine($"File{path} not found");
    }
    return text;
}
static void SaveFile(string path, string text)
{
    File.WriteAllText(path, text);
}
static Bitmap encrypt(string imagePath)
{
    Bitmap Image,
        newImage,
        imageKey;
    Color pixel,
        newPixel;
    Random seedGenerator = new Random();
    int seed = seedGenerator.Next();
    SaveFile("\\Users\\Leon\\source\\repos\\Vernamcipher\\Vernamcipher\\key.txt", seed.ToString());
    Random rnd = new Random(seed);

    Image = new Bitmap(imagePath);
    newImage = new Bitmap(Image.Width, Image.Height);
    imageKey = new Bitmap(Image.Width, Image.Height);
    for (int x = 0; x < Image.Width; x++)
    {
        for (int y = 0; y < Image.Height; y++)
        {
            pixel = Image.GetPixel(x, y);
            byte A = (byte)rnd.Next(255);
            byte R = (byte)rnd.Next(255);
            byte G = (byte)rnd.Next(255);
            byte B = (byte)rnd.Next(255);
            newImage.SetPixel(
                x,
                y,
                Color.FromArgb(
                    pixel.A ^ A,
                    pixel.R ^ R,
                    pixel.G ^ G,
                    pixel.B ^ B
                ));
            imageKey.SetPixel(x, y, Color.FromArgb(A, R, G, B));
        }
    }
    imageKey.Save("C:\\Users\\Leon\\source\\repos\\Vernamcipher\\Vernamcipher\\key.png");
    return newImage;
}
static Bitmap decrypt(int key, string imagePath)
{
    Random rnd = new Random(key);
    Bitmap Image,
        newImage;
    Image = new Bitmap(imagePath);
    newImage = new Bitmap(Image.Width, Image.Height);
    Color pixel;

    for (int x = 0; x < Image.Width; x++)
    {
        for (int y = 0; y < Image.Height; y++)
        {
            pixel = Image.GetPixel(x, y);
            newImage.SetPixel(
                x,
                y,
                Color.FromArgb(
                    pixel.A ^ rnd.Next(255),
                    pixel.R ^ rnd.Next(255),
                    pixel.G ^ rnd.Next(255),
                    pixel.B ^ rnd.Next(255)
                )
            );
        }
    }
    return newImage;
}
static Bitmap decryptImageKey(string keyPath, string imagePath)
{
    Bitmap Image,
        newImage,
        imageKey;
    Image = new Bitmap(imagePath);
    newImage = new Bitmap(Image.Width, Image.Height);
    imageKey = new Bitmap(keyPath);
    Color pixel;
    for (int x = 0; x < Image.Width; x++)
    {
        for (int y = 0; y < Image.Height; y++)
        {
            pixel = Image.GetPixel(x, y);
            newImage.SetPixel(
                x,
                y,
                Color.FromArgb(
                    pixel.A ^ imageKey.GetPixel(x, y).A,
                    pixel.R ^ imageKey.GetPixel(x, y).R,
                    pixel.G ^ imageKey.GetPixel(x, y).G,
                    pixel.B ^ imageKey.GetPixel(x, y).B
                )
            );
        }
        

    }
    return newImage;
}

    encrypt(imagePath)
    .Save("C:\\Users\\Leon\\source\\repos\\Vernamcipher\\Vernamcipher\\encrypted.png");
decrypt(
        int.Parse(LoadFile("C:\\Users\\Leon\\source\\repos\\Vernamcipher\\Vernamcipher\\key.txt")),
        "C:\\Users\\Leon\\source\\repos\\Vernamcipher\\Vernamcipher\\encrypted.png"
    )
    .Save("C:\\Users\\Leon\\source\\repos\\Vernamcipher\\Vernamcipher\\decryptedseed.png");
decryptImageKey
    ("C:\\Users\\Leon\\source\\repos\\Vernamcipher\\Vernamcipher\\key.png"
        ,
        "C:\\Users\\Leon\\source\\repos\\Vernamcipher\\Vernamcipher\\encrypted.png"
    )
    .Save("C:\\Users\\Leon\\source\\repos\\Vernamcipher\\Vernamcipher\\decryptedimage.png");

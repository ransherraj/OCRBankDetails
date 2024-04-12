//code

using AForge.Imaging.Filters;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Tesseract;
using static System.Net.Mime.MediaTypeNames;

namespace ConsoleAppOCR2
{
    public class Program
    {
        static void Main(string[] args)
        {
            // Call the OCR testing function
            OcrTesting();
            //OcrTesting2();
        }

        static void OcrTesting()
        {
            try
            {
                //Bitmap img = new Bitmap("./powerapps2.png");
                //Bitmap img = new Bitmap("./cheque.jpeg");
                // Load your image
                Bitmap originalImage = new Bitmap("./cheque3.jpg");

                /*// Apply enhancement filters
                Grayscale grayscaleFilter = new Grayscale(0.2125, 0.7154, 0.0721);
                Bitmap grayscaleImage = grayscaleFilter.Apply(originalImage);

                ContrastStretch contrastFilter = new ContrastStretch();
                contrastFilter.ApplyInPlace(grayscaleImage);

                // Apply noise reduction
                Median medianFilter = new Median();
                medianFilter.ApplyInPlace(grayscaleImage);*/

                //Tesseract Engine
                TesseractEngine engine = new TesseractEngine("./tessdata1", "eng", EngineMode.Default);
                Page page = engine.Process(originalImage, PageSegMode.Auto);
                string res = page.GetText();

                //res = res.ToLower();
                // Extract bank name, IFSC code, and account number
                string bankName = ExtractBankName(res);
                string ifscCode = ExtractIFSCCode(res);
                string accountNumber = ExtractAccountNumber(res);

                // Output extracted information
                Console.WriteLine("Bank Name: " + bankName);
                Console.WriteLine("IFSC Code: " + ifscCode);
                Console.WriteLine("Account Number: " + accountNumber);


                //Console.WriteLine(res);
                Console.ReadLine();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        static void OcrTesting2()
        {
            // Specify the path to the tessdata folder containing language data files
            //string tessdataPath = @"path\to\tessdata";
            string imagePath = @"path\to\image.png";

            // Load your image
            Bitmap originalImage = new Bitmap("./cheque.jpeg");

            // Apply enhancement filters
            Grayscale grayscaleFilter = new Grayscale(0.2125, 0.7154, 0.0721);
            Bitmap grayscaleImage = grayscaleFilter.Apply(originalImage);

            ContrastStretch contrastFilter = new ContrastStretch();
            contrastFilter.ApplyInPlace(grayscaleImage);

            // Apply noise reduction
            Median medianFilter = new Median();
            medianFilter.ApplyInPlace(grayscaleImage);

            // Save the processed image
            //grayscaleImage.Save("output.jpg");

            // Initialize Tesseract engine
            using (var engine = new TesseractEngine("./tessdata1", "eng", EngineMode.Default))
            {
                // Perform OCR on the image
                //using (var img = Pix.LoadFromFile("./cheque.jpeg"))
                using (var img = PixConverter.ToPix(grayscaleImage))
                {
                    using (var page = engine.Process(img))
                    {
                        // Get the extracted text
                        string extractedText = page.GetText();

                        // Output the extracted text
                        Console.WriteLine("Extracted Text:");
                        Console.WriteLine(extractedText);
                        Console.ReadLine();
                    }
                }
            }
        }


        static string ExtractBankName(string text)
        {
            // Match bank name pattern
            //Match match = Regex.Match(text, @"Bank Name: (.+)");
            /*Match match = Regex.Match(text, @"(?<=\n)[^\n]+(?=\n)");*/
            /*if (match.Success)
            {
                return match.Groups[1].Value;
            }*/
            Match match = Regex.Match(text, @"(?:\b\w+\b\s*)+");
            if (match.Success)
            {
                return match.Value.Trim();
            }
            return "Bank Name Not Found";
        }

        static string ExtractIFSCCode(string text)
        {
            // Match IFSC code pattern
            /*Match match1 = Regex.Match(text, @"ifsc code: ([A-Z0-9]+)");
            Match match2 = Regex.Match(text, @"ifs code: ([A-Z0-9]+)");
            Match match3 = Regex.Match(text, @"ifs code : ([A-Z0-9]+)");
            Match match4 = Regex.Match(text, @"ifsc : ([A-Z0-9]+)");
            Match match5 = Regex.Match(text, @"ifsc: ([A-Z0-9]+)");
            Match match6 = Regex.Match(text, @"ifs: ([A-Z0-9]+)");*/

            Match match1 = Regex.Match(text, @"IFSC Code: ([A-Z0-9]+)");
            Match match2 = Regex.Match(text, @"IFS Code: ([A-Z0-9]+)");
            Match match3 = Regex.Match(text, @"IFS Code : ([A-Z0-9]+)");
            Match match4 = Regex.Match(text, @"IFSC : ([A-Z0-9]+)");
            Match match5 = Regex.Match(text, @"IFSC: ([A-Z0-9]+)");
            Match match6 = Regex.Match(text, @"IFS: ([A-Z0-9]+)");
            if (match1.Success)
            {
                return match1.Groups[1].Value;
            }
            else if (match2.Success)
            {
                return match2.Groups[1].Value;
            }
            else if (match3.Success)
            {
                return match3.Groups[1].Value;
            }
            else if (match4.Success)
            {
                return match4.Groups[1].Value;
            }
            else if (match5.Success)
            {
                return match5.Groups[1].Value;
            }
            else if (match6.Success)
            {
                return match6.Groups[1].Value;
            }
            return "IFSC Code Not Found";
        }

        static string ExtractAccountNumber(string text)
        {
            // Match account number pattern
            Match match1 = Regex.Match(text, @"A/C: (\d+)");
            Match match2 = Regex.Match(text, @"A/C : (\d+)");
            Match match3 = Regex.Match(text, @"acc number: (\d+)");
            Match match4 = Regex.Match(text, @"account number: (\d+)");
            Match match5 = Regex.Match(text, @"account number: (\d+)");
            Match match6 = Regex.Match(text, @"a/c: (\d+)");
            Match match7 = Regex.Match(text, @"a/c : (\d+)");
            if (match1.Success)
            {
                return match1.Groups[1].Value;
            }
            else if (match2.Success)
            {
                return match2.Groups[1].Value;
            }
            else if (match3.Success)
            {
                return match3.Groups[1].Value;
            }
            else if (match4.Success)
            {
                return match4.Groups[1].Value;
            }
            else if (match5.Success)
            {
                return match5.Groups[1].Value;
            }
            else if (match6.Success)
            {
                return match6.Groups[1].Value;
            }
            else if (match7.Success)
            {
                return match7.Groups[1].Value;
            }
            return "Account Number Not Found";
        }
    }
}

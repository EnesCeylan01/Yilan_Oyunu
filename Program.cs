using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

class Program
{
    static void Main()
    {   // Oyunun başlangıç ayarlarını yapar boyutunu belirler ekranın
        Console.WindowHeight = 30;
        Console.WindowWidth = 80;
        // Oyun tahtasının genişlik ve yüksekliğini al
        int screenwidth = Console.WindowWidth;
        int screenheight = Console.WindowHeight;
        Random randomnummer = new Random();
        // Yılanın başını temsil eden bir nesne oluştur
        pixel hoofd = new pixel();
        hoofd.xpos = screenwidth / 2;
        hoofd.ypos = screenheight / 2;
        hoofd.schermkleur = ConsoleColor.Red;
        // Yılanın hareket yönünü belirleyen bir değişken
        string movement = "RIGHT";
        List<int> xposlijf = new List<int>();
        List<int> yposlijf = new List<int>();
        // Oyun skorunu ve yem pozisyonunu takip etmek için değişkenler
        int score = 0;
        int foodxpos, foodypos;
        foodxpos = randomnummer.Next(1, screenwidth);
        foodypos = randomnummer.Next(1, screenheight);
        DateTime tijd = DateTime.Now;
        // Yılanın başlangıçta hareketsiz olması için bir tuş durumu
        string buttonpressed = "no";
        // Ana oyun döngüsü
        while (true)
        {
            // Yılanın ve yemin görüntüsünü ekrana yazdır
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Score:" + score);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.Write("☺");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.SetCursorPosition(foodxpos, foodypos);
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.Write("☻");
            // Oyunun skorunu ekrana yazdır
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.SetCursorPosition(1, screenheight);
            Console.WriteLine("");
            Console.SetCursorPosition(1, screenheight + 1);
            Console.WriteLine("");
            Console.SetCursorPosition(1, screenheight + 2);
            Console.WriteLine("Çıkmak İçin X Tuşuna Basınız");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.SetCursorPosition(hoofd.xpos, hoofd.ypos);
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.Write("■");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.SetCursorPosition(foodxpos, foodypos);
            Console.Write("☻");
            Console.ForegroundColor = ConsoleColor.White;
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.SetCursorPosition(3, screenheight);
            Console.Write("Yön Tuşları İle Hareket Edebilirsiniz");
            Console.SetCursorPosition(2, screenheight + 1);
            Console.Write("Her Yemi Yediğinde Yılanın Boyutu Artacaktır");
            Console.SetCursorPosition(12, screenheight + 3);
            Console.Write("Enes Ceylan Saygılar Sunar");

            ConsoleKeyInfo toets = Console.ReadKey();
            //Burada basılan tuşa bakıyoruz ona göre yılan hareket ediyor
            switch (toets.Key)
            {
                case ConsoleKey.UpArrow:
                    buttonpressed = "UP";
                    break;
                case ConsoleKey.DownArrow:
                    buttonpressed = "DOWN";
                    break;
                case ConsoleKey.LeftArrow:
                    buttonpressed = "LEFT";
                    break;
                case ConsoleKey.RightArrow:
                    buttonpressed = "RIGHT";
                    break; 
                case ConsoleKey.X:
                    Environment.Exit(0); // X tuşuna basılınca uygulamayı kapat
                    break;
            }
            Console.CursorVisible = false;
            //Burada yılanı basılan tuşun yönetiminde hareket ettiriyoruz
            if (buttonpressed == "UP")
            {
                hoofd.ypos--;
            }
            if (buttonpressed == "DOWN")
            {
                hoofd.ypos++;
            }
            if (buttonpressed == "LEFT")
            {
                hoofd.xpos--;
            }
            if (buttonpressed == "RIGHT")
            {
                hoofd.xpos++;
            }
            //burada yılanın ekran içinde kalmasını sağlıyoruz Bu kontroller, yılanın oyun tahtasının sınırlarını aşmamasını sağlar.
            if (hoofd.xpos < 0) hoofd.xpos = screenwidth - 1;
            if (hoofd.xpos > screenwidth - 1) hoofd.xpos = 0;
            if (hoofd.ypos < 0) hoofd.ypos = screenheight - 1;
            if (hoofd.ypos > screenheight - 1) hoofd.ypos = 0;

            // Burada yılanın kendi vücudu üzerine geldiğini kontrol ediyoruz.
            for (int i = 0; i < xposlijf.Count; i++)
            {
                Console.SetCursorPosition(xposlijf[i], yposlijf[i]);
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.OutputEncoding = System.Text.Encoding.UTF8;
                Console.Write("■");

                if (xposlijf[i] == hoofd.xpos && yposlijf[i] == hoofd.ypos)
                {
                    Console.Clear();
                    Console.WriteLine("Yandınız! Oyun Başa Dönüyor...");
                    Thread.Sleep(2000);
                    Main();
                }
            }

            // Burada yılanın başının veya vücudunun üzerine gelip gelmediğini kontrol ediyoruz.
            if (hoofd.xpos == foodxpos && hoofd.ypos == foodypos)
            {
                score++;
                foodxpos = randomnummer.Next(1, screenwidth);
                foodypos = randomnummer.Next(1, screenheight);
                xposlijf.Insert(0, hoofd.xpos);
                yposlijf.Insert(0, hoofd.ypos);
            }
            else
            {
                xposlijf.Insert(0, hoofd.xpos);
                yposlijf.Insert(0, hoofd.ypos);
                xposlijf.RemoveAt(xposlijf.Count - 1);
                yposlijf.RemoveAt(yposlijf.Count - 1);
            }
            // Ekranı belirli bir süre beklet (oyunun hızını kontrol etmek için)
            Thread.Sleep(100);
        }
    }
}
// Yılanın ve yemin piksel temsilini sağlayan sınıf
//Bu aşamada yılanın başını (☺) ve yemin pozisyonunu (foodxpos ve foodypos) ekrana çizdik.
public class pixel 
{
    public int xpos { get; set; }
    public int ypos { get; set; }
    public ConsoleColor schermkleur { get; set; }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Mime;
using System.Text;
using System.Windows;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Lab09
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        
        public MainWindow()
        {
            InitializeComponent();
            
            
        }

        private void Reload(int pgn, int pgs, string snpt)
        {
            List<SnippetReponse> SR = new List<SnippetReponse>();

            int pageNumber = pgn;
            int pageSize = pgs;
            string snippetsType = snpt;

            PageReposne reponse = FetchSnippets(pageNumber, pageSize, snippetsType);

            foreach (SnippetReponse snippet in reponse.Batches)
            {
                SR.Add(new SnippetReponse()
                {
                    Size = snippet.Size,
                    Name = snippet.Name,
                    Type = snippet.Type,
                    CreationTime = snippet.CreationTime,
                    UpdateTime = snippet.UpdateTime
                });
            }
            response.ItemsSource = SR;
        }
        
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            List<SnippetReponse> SR = new List<SnippetReponse>();

            int pageNumber = 1;
            int pageSize = 5;
            string snippetsType = "cs";

            PageReposne reponse = FetchSnippets(pageNumber, pageSize, snippetsType);

            foreach (SnippetReponse snippet in reponse.Batches)
            {
                SR.Add(new SnippetReponse()
                {
                    Size = snippet.Size,
                    Name = snippet.Name,
                    Type = snippet.Type,
                    CreationTime = snippet.CreationTime,
                    UpdateTime = snippet.UpdateTime
                });
            }
            response.ItemsSource = SR;

            

            Console.WriteLine($"PageNumber: {reponse.PageNumber}");
            Console.WriteLine($"PagesCount: {reponse.PagesCount}");
            Console.WriteLine($"PageSize:   {reponse.PageSize}  ");
            Console.WriteLine($"TotalCount: {reponse.TotalCount}");

            Console.WriteLine();
            Console.WriteLine("Snippets:");

            foreach (SnippetReponse snippet in reponse.Batches)
                Console.WriteLine("    (" + snippet.Size + ")    " + snippet.Name + "    [" + snippet.Type + "]    " + snippet.CreationTime + "    " + snippet.UpdateTime);

            // Example ouput:
            //
            //     PageNumber: 1
            //     PagesCount: 12
            //     PageSize:   5
            //     TotalCount: 60
            //     
            //     Snippets:
            //         (1)    C# - fetch current Dirask snippets and display in console    [cs]    2022-05-27 03:23:49    2022-05-27 04:42:43
            //         (2)    C# - request web URL and get response content                [cs]    2022-05-27 02:05:36
            //         (1)    C# - async/await methods with synchronization                [cs]    2022-05-20 11:25:59    2022-05-20 21:16:17
            //         (2)    C# - lock statement usage example                            [cs]    2022-05-20 09:46:34    2022-05-22 01:37:16
            //         (2)    console.log() in C#                                          [cs]    2022-05-18 17:05:41    2022-05-22 19:09:07
        }

        public string last = "cs";
        public int pgslast = 5;
        public static string FetchData(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                ContentType type = new ContentType(response.ContentType ?? "text/plain;charset=" + Encoding.UTF8.WebName);
                Encoding encoding = Encoding.GetEncoding(type.CharSet ?? Encoding.UTF8.WebName);

                using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream, encoding))
                {
                    return reader.ReadToEnd();
                }
            }
        }

        public static PageReposne FetchSnippets(int pageNumber, int pageSize, string snippetsType)
        {
            string url = $"https://dirask.com/api/snippets?pageNumber={pageNumber}&pageSize={pageSize}&dataOrder=newest&dataGroup=batches&snippetsType={Uri.EscapeUriString(snippetsType)}";
            string data = FetchData(url);

            return JsonSerializer.Deserialize<PageReposne>(data);
        }

        private void btnBash_Click(object sender, RoutedEventArgs e)
        {
            Reload(1, pgslast, "bash");
            last = "bash";
        }

        private void btnText_Click(object sender, RoutedEventArgs e)
        {
            Reload(1, pgslast, "text");
            last = "text";
        }

        private void btnCplus_Click(object sender, RoutedEventArgs e)
        {
            Reload(1, pgslast, "cpp");
            last = "cpp";
        }

        private void btnCsharp_Click(object sender, RoutedEventArgs e)
        {
            Reload(1, pgslast, "cs");
            last = "cs";
        }

        private void btnJava_Click(object sender, RoutedEventArgs e)
        {
            Reload(1, pgslast, "java");
            last = "java";
        }

        private void btnCSS_Click(object sender, RoutedEventArgs e)
        {
            Reload(1, pgslast, "css");
            last = "css";
        }

        private void btnHtml_Click(object sender, RoutedEventArgs e)
        {
            Reload(1, pgslast, "html");
            last = "html";
        }

        private void btnJS_Click(object sender, RoutedEventArgs e)
        {
            Reload(1, pgslast, "javascript");
            last = "javascript";
        }

        private void btnPhp_Click(object sender, RoutedEventArgs e)
        {
            Reload(1, pgslast, "php");
            last = "php";
        }

        private void btnPython_Click(object sender, RoutedEventArgs e)
        {
            Reload(1, pgslast, "python");
            last = "python";
        }

        private void btnSQL_Click(object sender, RoutedEventArgs e)
        {
            Reload(1, pgslast, "sql");
            last = "sql";
        }

        private void btnOne_Click(object sender, RoutedEventArgs e)
        {
            Reload(1, pgslast, last);
            
        }

        private void btnTwo_Click(object sender, RoutedEventArgs e)
        {
            Reload(2, pgslast, last);
        }

        private void btnThree_Click(object sender, RoutedEventArgs e)
        {
            Reload(3, pgslast, last);
        }

        private void btnFour_Click(object sender, RoutedEventArgs e)
        {
            Reload(4, pgslast, last);
        }

        private void btnFive_Click(object sender, RoutedEventArgs e)
        {
            Reload(5, pgslast, last);
        }




       /* private void comboboxx_DropDownClosed(object sender, EventArgs e)
        {

        }
       */
  


        private void ComboBoxItem_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            Reload(1, 5, last);
            pgslast = 5;
        }

        private void ComboBoxItem_PreviewMouseDown_1(object sender, MouseButtonEventArgs e)
        {
            Reload(1, 10, last);
            pgslast = 10;
        }

        private void ComboBoxItem_PreviewMouseDown_2(object sender, MouseButtonEventArgs e)
        {
            Reload(1, 20, last);
            pgslast = 10;
        }
    }


    public class PageReposne
    {
        [JsonPropertyName("pageNumber")]
        public int PageNumber { get; set; }

        [JsonPropertyName("pagesCount")]
        public int PagesCount { get; set; }

        [JsonPropertyName("pageSize")]
        public int PageSize { get; set; }

        [JsonPropertyName("totalCount")]
        public int TotalCount { get; set; }

        [JsonPropertyName("batches")]
        public List<SnippetReponse> Batches { get; set; }
    }

    public class SnippetReponse
    {
        [JsonPropertyName("size")]
        public int Size { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("creationTime")]
        public DateTime? CreationTime { get; set; }

        [JsonPropertyName("updateTime")]
        public DateTime? UpdateTime { get; set; }
    }


       
 }


using Buoi02;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buoi02
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Xuất text theo Unicode (có dấu tiếng Việt)
            Console.OutputEncoding = Encoding.Unicode;
            // Nhập text theo Unicode (có dấu tiếng Việt)
            Console.InputEncoding = Encoding.Unicode;

            /* Tạo menu */
            Menu menu = new Menu();
            string title = "VẬN DỤNG CÁC THAO TÁC CƠ BẢN TRÊN ĐỒ THỊ";   // Tiêu đề menu
            // Danh sách các mục chọn
            string[] ms = { "1. Bài 1: Chuyển danh sách cạnh sang danh sách kề",
                "2. Bài 2: Chuyển danh sách kề sang danh sách cạnh",
                "3. Bài 3: Đỉnh Bồn chứa",
                "4. Bài 4: Đồ thị chuyển vị",
                "5. Bài 5: Độ dài trung bình của cạnh",
                "0. Thoát" };
            int chon;
            do
            {
                // Xuất menu
                menu.ShowMenu(title, ms);
                Console.Write("     Chọn : ");
                chon = int.Parse(Console.ReadLine());
                switch (chon)
                {
                    case 1:
                        {   // Bài 1: Chuyển danh sách cạnh sang danh sách kề
                            // Tạo đường dẫn fileInput -> đồ thị EdgeList
                            string fileInput = "../../TextFile/EdgeList.txt";
                            EdgeList ge = new EdgeList();
                            ge.FileToEdgeList(fileInput); ge.Output();
                            // Tạo đồ thị AdjList ga từ EdgeList ge
                            AdjList ga = new AdjList();
                            ga = EdgeListToAdjList(ge); ga.Output();
                            // Tạo đường dẫn fileOutput
                            string fileOutput = "../../TextFile/AdjList.txt";
                            ga.AdjListToFile(fileOutput);
                            break;
                        }
                    case 2:
                        {

                            break;
                        }
                    case 3:
                        {
                            // Bài 3 : Bồn chứa
                            // Khởi tạo g là đồ thị ma trận kề : AdjMatrix g
                            AdjMatrix g=new AdjMatrix();
                            // Tạo đường dẫn fileInput : DirectedMatrix.txt
                            string fileInput = "../../TextFile/DirectedMatrix.txt";
                            // Tạo đồ thị g và xuất đồ thị g lên màn hình
                            g.FileToAdjMatrix(fileInput);
                            g.Output();
                            // Tạo đường dẫn fileOutput : Storage.txt
                            string fileOutput = "../../Storage.txt";
                            // Gọi hàm : Storage(g, fileOutput);
                            Storage(g, fileOutput);
                            break;
                        }
                    case 4:
                        {
                            // Bài 4 : Đồ thị chuyển vị
                            // Tạo đường dẫn file Input : "../../../TextFile/DirectedList.txt";
                            string fileInput = "../../TextFile/DirectedList.txt";
                            // Khai báo đồ thị g : AdjList g = new AdjList();
                            AdjList g = new AdjList();
                            // Tạo đồ thị từ fileInput và xuất đồ thị
                            g.FileToAdjList(fileInput); g.Output();
                            // Khai báo G là đồ thị chuyển vị : AdjList G = new AdjList();
                            AdjList G = new AdjList();
                            // Gọi hàm : G = TransposeG(g); G.Output();
                            G = TransposeG(g);
                            // Xuất đồ thi chuyển vị G lên màn hình
                            G.Output();
                            // SV tự làm ghi kết quả vào file Transpose.txt
                            string fileOutput = "../../TextFile/Transpose.txt";
                            G.AdjListToFile(fileOutput);
                            break;
                        }
                    case 5:
                        {
                            // Tạo tham số fileInput
                            string fileInput = "../../TextFile/WeightEdgeList.txt";
                            WeightEdgeList g = new WeightEdgeList();
                            g.FileToWeightEdgeList(fileInput); g.Output();
                            Console.Write("Cạnh dài nhất: "); MaxEdge(g);
                            Console.WriteLine("Chiều dài TB : {0:0.00}", AverageEdge(g));
                            break;
                        }

                }
                Console.WriteLine(" Nhấn một phím bất kỳ");
                Console.ReadKey();
                Console.Clear();
            } while (chon != 0);
        }
        // Chuyển file EdgeList sang đồ thị danh sách kề AdjList
        // Chuyển file EdgeList sang đồ thị danh sách kề AdjList
        static AdjList EdgeListToAdjList(EdgeList ge)
        {
            // Khởi tạo đồ thị AdjList
            AdjList ga = new AdjList();
            // Xác định số đỉnh n của đồ thị : ga.N = ge.N;
            ga.N = ge.N;
            // Khởi tạo array v[] của đồ thị AdjList
         LinkedList<int >v= new LinkedList<int>();
            // Khởi tạo các danh sách liên kết ga.V[i]
            ga.V = new LinkedList<int>[ga.N];
            // Duyệt từng đỉnh i trong ga , i = 0..ga.N-1
            for (int i = 0; i < ga.N; i++)
            {

                // Khởi tạo các dslk : ga.V[i] = new LinkedList<int>();
                ga.V[i] = new LinkedList<int>();
            }
            // Xây dựng các phấn tử cho các dslk
            // Duyệt từng cạnh e trong đồ thị EdgeList
            foreach(Tuple<int,int> e in ge.G )
            {
                // AddLast e.Item2 vào ga.V[e.Item1]
                ga.V[e.Item1].AddLast(e.Item2);
                // AddLast e.Item1 vào ga.V[e.Item2]
                ga.V[e.Item2].AddLast(e.Item1);
            }
            return ga;	// Đồ thị trả về
        }
        static void Storage(AdjMatrix g, string fileOut)
        {
            // Khởi tạo : StreamWriter sw
            StreamWriter sw = new StreamWriter(fileOut);
            // Khai báo biến đếm : count=0
            int count = 0;
            // Duyệt các đỉnh i của g
            for(int i=0; i < g.N; i++)
            {
                // Nếu (g.IsStorage(i) == true)
                if(g.IsStorage(i)==true) 
                {
                    count++;
                    // đếm count lên 1
                    // Xuất lên màn hình : ("Đỉnh " + i + " là đỉnh bồn chứa");
                    Console.WriteLine($"Đỉnh " + i + " là đỉnh bồn chứa ");
                    // Ghi file sw : ("Đỉnh " + i + " là đỉnh bồn chứa");
                    sw.Write($"Đỉnh " + i + " là đỉnh bồn chứa");
                }
            }
            // Ghi file sw : ("Số đỉnh là bồn chứa : " + count);
            sw.WriteLine("Số đỉnh là bồn chứa : " + count);
            // Xuất lên màn hình : ("Số đỉnh là bồn chứa : " + count);
            Console.WriteLine("Số đỉnh là bồn chứa : " + count);
            // Đóng file sw
            sw.Close();
        }
        static AdjList TransposeG(AdjList g)
        {
            // Khai báo đồ thị G : AdjList G
            AdjList G = new AdjList();
            // Xác định số đỉnh G.N là số đỉnh g.N
            G.N = g.N;
            // Cấp phát vùng nhớ cho G.V : new LinkedList<int>[G.N];
            G.V = new LinkedList<int>[G.N];
            // Khởi tạo các dslk G.V[i] = new LinkedList<int>() , i = 0..G.N-1
            for (int i = 0; i < G.N; i++)
            {
                G.V[i] = new LinkedList<int>();
            }
            // Duyệt từng đỉnh i của G
            for (int i = 0; i < g.N; i++)
            {
                // Duyệt từng đỉnh x trong G.V[i]
                foreach (int x in g.V[i])
                {// AddLast i vào G.V[x] : G.V[x].AddLast(i);
                    G.V[x].AddLast(i);
                }
            }
            // Trả về G
            return G;
        }
        // Độ dài nhất và các cạnh có độ dài lớn nhất
        static void MaxEdge(WeightEdgeList g)
        {
            int max = -int.MaxValue;
            // Dùng một dslk lst dùng chứa các cạnh dài nhất
            LinkedList<Tuple<int, int, int>> lst = new LinkedList<Tuple<int, int, int>>();
            // Tìm độ dài nhất max, trong g.G
            foreach (var e in g.G)
            {
                if (e.Item3 > max)
                {
                    max = e.Item3;
                }
            }
            // Tìm các cạnh dài nhất (item3 = max) và Add vào lst
            foreach (var e in g.G)
            {
                if (e.Item3 == max)
                {
                    lst.AddLast(e);
                }
            }
            // Xuất các cạnh dài nhất & số lượng
            Console.WriteLine(lst.Count + " cạnh");
            foreach (Tuple<int, int, int> e in lst)
                Console.WriteLine("      (" + e.Item1 + "," + e.Item2 + ") = " + e.Item3);
        }
        // Trung bình cạnh
        static double AverageEdge(WeightEdgeList g)
        {
            double sum = 0;
            foreach (var edge in g.G)
            {
                sum += edge.Item3;
            }
            double avg = sum / g.G.Count;
            return avg;
        }

    }
}


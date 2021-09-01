using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization.Json;
using System.IO;
using System.Text;

namespace ConsoleApp2
{
    class Program
    {
        class Matrix : IComparable<Matrix>
        {
            public double[,] matrix;

            public double Determinant => matrix[0, 0] * matrix[1, 1] - matrix[0, 1] * matrix[1, 0];

            public Matrix(double[,] matrix)
            {
                int cnt = 0;
                foreach (double el in matrix)
                {
                    cnt++;
                }

                if (cnt != 4)
                {
                    throw new Exception("Constructor error!");
                }

                this.matrix = matrix;

            }

            public double this[int index1, int index2]
            {
                get
                {
                    if (index1 < 2 && index1 >= 0 && index2 < 2 && index2 >= 0)
                    {
                        return this.matrix[index1, index2];
                    }
                    else
                    {
                        throw new Exception("Index out of range!");
                    }

                }
                set
                {
                    if (index1 < 2 && index1 >= 0 && index2 < 2 && index2 >= 0)
                    {
                        this.matrix[index1, index2] = value;
                    }
                    else
                    {
                        throw new Exception("Index out of range!");
                    }
                }
            }

            public Matrix InverseMatrix()
            {
                Matrix inverse_matrix = new Matrix(new double[2, 2]);

                if (this.Determinant != 0)
                {
                    for (int i = 0; i < 2; i++)
                    {
                        for (int j = 0; j < 2; j++)
                        {
                            inverse_matrix[j, i] = this.matrix[i, j] / this.Determinant;
                        }
                    }

                    return inverse_matrix;


                }

                throw new Exception("Determinant is zero!");

            }

            public override string ToString()
            {
                string result = "";
                for (int i = 0; i < 2; i++)
                {
                    for (int j = 0; j < 2; j++)
                    {
                        if (j == 0)
                        {
                            result += this.matrix[i, j];
                        }
                        else
                        {
                            result += " " + this.matrix[i, j];
                        }


                    }

                    if (i != 1)
                    {
                        result += '\n';
                    }


                }

                return result;
            }

            public static Matrix Parse(string str)
            {
                Matrix matrix = new Matrix(new double[2, 2]);
                if (!TryParse(str, out matrix))
                {
                    throw new Exception("String is invalid for parse to matrix!");

                }

                return matrix;



            }

            public static bool TryParse(string str, out Matrix matrix)
            {
                matrix = new Matrix(new double[2, 2]);
                string[] rows = str.Split("\n");
                if (rows.Length != 2)
                {
                    return false;
                }

                foreach (string s in rows)
                {
                    string[] coloumns = s.Split(" ");
                    if (coloumns.Length != 2)
                    {
                        return false;
                    }

                    foreach (string item in coloumns)
                    {
                        double el;
                        if (!Double.TryParse(item, out el))
                        {
                            return false;
                        }

                    }

                }

                for (int i = 0; i < rows.Length; i++)
                {
                    string[] coloumns = rows[i].Split(" ");
                    for (int j = 0; j < coloumns.Length; j++)
                    {
                        matrix[i, j] = Double.Parse(coloumns[j]);

                    }
                }

                return true;
            }

            public static Matrix operator -(Matrix m1, Matrix m2)
            {
                Matrix matrix = new Matrix(new double[2, 2]);
                for (int i = 0; i < 2; i++)
                {
                    for (int j = 0; j < 2; j++)
                    {
                        matrix[i, j] = m1[i, j] - m2[i, j];
                    }
                }

                return matrix;
            }

            public static Matrix operator +(Matrix m1, Matrix m2)
            {
                Matrix matrix = new Matrix(new double[2, 2]);
                for (int i = 0; i < 2; i++)
                {
                    for (int j = 0; j < 2; j++)
                    {
                        matrix[i, j] = m1[i, j] + m2[i, j];
                    }
                }

                return matrix;
            }

            public static Matrix operator ++(Matrix m1)
            {
                Matrix matrix = new Matrix(new double[2, 2]);
                for (int i = 0; i < 2; i++)
                {
                    for (int j = 0; j < 2; j++)
                    {
                        matrix[i, j] = m1[i, j] + 1;
                    }
                }

                return matrix;
            }

            public static Matrix operator --(Matrix m1)
            {
                Matrix matrix = new Matrix(new double[2, 2]);
                for (int i = 0; i < 2; i++)
                {
                    for (int j = 0; j < 2; j++)
                    {
                        matrix[i, j] = m1[i, j] - 1;
                    }
                }

                return matrix;
            }


            public static bool operator >(Matrix m1, Matrix m2)
            {
                return m1.Determinant > m2.Determinant;
            }

            public static bool operator <(Matrix m1, Matrix m2)
            {
                return m1.Determinant < m2.Determinant;
            }

            public static bool operator >=(Matrix m1, Matrix m2)
            {
                return m1.Determinant >= m2.Determinant;
            }

            public static bool operator <=(Matrix m1, Matrix m2)
            {
                return m1.Determinant <= m2.Determinant;
            }

            public static bool operator ==(Matrix m1, Matrix m2)
            {
                return m1.matrix == m2.matrix;
            }

            public static bool operator !=(Matrix m1, Matrix m2)
            {
                return m1.matrix != m2.matrix;
            }

            public static Matrix operator *(Matrix m1, Matrix m2)
            {

                Matrix matrix = new Matrix(new double[2, 2]);

                for (var i = 0; i < 2; i++)
                {
                    for (var j = 0; j < 2; j++)
                    {
                        for (var k = 0; k < 2; k++)
                        {
                            matrix[i, j] += m1[i, k] * m2[k, j];
                        }
                    }
                }

                return matrix;
            }

            public int CompareTo(Matrix compareParMatrix)
            {
                // A null value means that this object is greater.
                if (compareParMatrix == null)
                    return 1;

                else
                    return this.Determinant.CompareTo(compareParMatrix.Determinant);
            }



            public static Matrix operator /(Matrix m1, Matrix m2)
            {

                Matrix matrix = new Matrix(new double[2, 2]);
                Matrix inverse_matrix = m2.InverseMatrix();

                for (var i = 0; i < 2; i++)
                {
                    for (var j = 0; j < 2; j++)
                    {
                        for (var k = 0; k < 2; k++)
                        {
                            matrix[i, j] += m1[i, k] * inverse_matrix[k, j];
                        }
                    }
                }

                return matrix;
            }
            


        }

        class ListOfMatrix
        {
            private List<Matrix> list;
            public int Count = 0;
            public bool IsSorted = false;

            public ListOfMatrix()
            {
                this.list = new List<Matrix>();
            }
            
            public Matrix this[int index]
            {
                get
                {
                    if (index < list.Count && index >= 0)
                    {
                        return list[index];
                    }
                    else
                    {
                        throw new Exception("Index out of range!");
                    }

                }
                set
                {
                    if (index < list.Count && list.Count >= 0)
                    {
                        list[index] = value;
                    }
                    else
                    {
                        throw new Exception("Index out of range!");
                    }
                }
            }

            public void Sort()
            {
                if (this.IsSorted)
                {
                    return;
                }
                this.IsSorted = true;
                list.Sort(delegate(Matrix matrix, Matrix matrix1)
                {
                    return matrix.Determinant.CompareTo(matrix1.Determinant);
                });
            }

            public Matrix Last()
            {
                return list.Last();
            }

            public Matrix First()
            {
                return list.First();
            }

            public Matrix Max()
            {
                Matrix ans = list[0];
                foreach (Matrix matrix in this.list)
                {
                    if (matrix.Determinant > ans.Determinant)
                    {
                        ans = matrix;
                    }
                    
                }

                return ans;
            }
            
            public Matrix Min()
            {
                Matrix ans = list[0];
                foreach (Matrix matrix in this.list)
                {
                    if (matrix.Determinant < ans.Determinant)
                    {
                        ans = matrix;
                    }
                    
                }

                return ans;
            }

            public void Add(Matrix matrix)
            {
                this.IsSorted = false;
                this.list.Add(matrix);
                this.Count++;
            }
            
            public Matrix[] ToArray()
            {
                Matrix[] arr = new Matrix[this.list.Count];
                for (int i = 0; i < this.list.Count; i++)
                {
                    arr[i] = this.list[i];
                }

                return arr;
            }
            
            public string[] ToStringArray()
            {
                string[] arr = new string[this.list.Count];
                for (int i = 0; i < this.list.Count; i++)
                {
                    arr[i] = this.list[i].ToString();
                }

                return arr;
            }
            


        }
        



        static class MatrixInOut
        {
            public static ListOfMatrix ReadMatrixFromFile(string path)
            {
                try
                {
                    using (StreamReader sr = new StreamReader(path))
                    {
                        string data = sr.ReadToEnd().Trim('\n', '-');
                        string[] arr = data.Split("\n+\n");
                        ListOfMatrix list = new ListOfMatrix();
                        foreach (string matrix in arr)
                        {
                            list.Add(Matrix.Parse(matrix));
                        }

                        return list;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

                return null;
            }
            
            public static bool WriteMatrixToFile(string path, ListOfMatrix list)
            {
                try
                {
                    using (StreamWriter sw = new StreamWriter(path, false, System.Text.Encoding.Default))
                    {
                        for (int i = 0; i < list.Count; i++)
                        {
                            sw.WriteLine(list[i].ToString());
                            if (i != list.Count - 1)
                            {
                                sw.WriteLine('+');
                            }
                            else
                            {
                                sw.WriteLine('-');
                            }
                        }
                       
                        return true;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

                return false;
            }
            
        }

        static ListOfMatrix Action(ListOfMatrix list, int n)
        {
            ListOfMatrix res = new ListOfMatrix();
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].Determinant < n)
                {
                    res.Add(list[i]);
                }
            }

            return res;
            
        }
        
        static void Main(string[] args)
        {
            int n;
            Console.Write("Введите определитель:");
            if (!Int32.TryParse(Console.ReadLine(), out n))
            {
                Console.WriteLine("Некорректные данные!");
                return;
            }
            ListOfMatrix list = new ListOfMatrix();
            Console.Write("Выберите метод ввода введя соответствующую цифру: \n1 - Консоль\n2 - Файл\n");
            int MethodIn;
            if (!Int32.TryParse(Console.ReadLine(), out MethodIn))
            {
                Console.WriteLine("Некорректные данные!");
                return;
            }
            Console.Write("Выберите метод вывода введя соответствующую цифру: \n1 - Консоль\n2 - Файл\n");
            int MethodOut;
            if (!Int32.TryParse(Console.ReadLine(), out MethodOut))
            {
                Console.WriteLine("Некорректные данные!");
                return;
            }

            if (MethodIn == 2)
            {
                Console.WriteLine("Введите путь:");
                string pathIn = Console.ReadLine();
                list = MatrixInOut.ReadMatrixFromFile(pathIn);
                list = Action(list, n);
                list.Sort();
                if (MethodOut == 1)
                {
                    Console.WriteLine(list.ToString());
                }
                else if (MethodOut == 2)
                {
                    string pathOut = Console.ReadLine();
                    MatrixInOut.WriteMatrixToFile(pathOut, list);
                }
                else
                {
                    Console.WriteLine("Некорректные данные!");
                    return;
                }
                
            }
            else if (MethodIn == 1)
            {
                Console.WriteLine("Ведите матрицы разделяя матрицы знаком +, в конце введите на отдельной строчке знак -:");
                StringBuilder sb = new StringBuilder();
                string line = "";
                do
                {
                    line = Console.ReadLine();
                    sb.AppendLine(line);
                } while (line != "-");

                string data = sb.ToString().Trim('\n', '-');
                string[] arr = data.Split("\n+\n");
                foreach (string matrix in arr)
                {
                    Matrix res = Matrix.Parse(matrix);
                    list.Add(res);
                }

                list = Action(list, n);
                list.Sort();
                if (MethodOut == 1)
                {
                    for (int i = 0; i < list.Count; i++)
                    {
                        Console.WriteLine(list[i]);
                        Console.WriteLine("-------");
                    }
                }
                else if (MethodOut == 2)
                {
                    string pathOut = Console.ReadLine();
                    MatrixInOut.WriteMatrixToFile(pathOut, list);
                }
                else
                {
                    Console.WriteLine("Некорректные данные!");
                    return;
                }
                
                
                
            }
            else
            {
                Console.WriteLine("Некорректные данные!");
                return;
            }

        }
    }
}
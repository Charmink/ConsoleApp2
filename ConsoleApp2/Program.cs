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
        class Matrix: IComparable<Matrix>
        {
            public double[,] matrix;
            private string string_interpr;

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

            public double Determinant() => this.matrix[0, 0] * this.matrix[1, 1] - this.matrix[0, 1] * this.matrix[1, 0];

            public Matrix InverseMatrix()
            {
                Matrix inverse_matrix = new Matrix(new double[2,2]);
                double det = this.matrix[0, 0] * this.matrix[1, 1] - this.matrix[0, 1] * this.matrix[1, 0];

                if (det != 0)
                {
                    for (int i = 0; i < 2; i++)
                    {
                        for (int j = 0; j < 2; j++)
                        {
                            inverse_matrix[j, i] = this.matrix[i, j] / det;
                        }
                    }

                    return inverse_matrix;


                }
                throw new Exception("Determinant is zero!");

            }

            public override string ToString()
            {
                this.string_interpr = "";
                for (int i = 0; i < 2; i++)
                {
                    for (int j = 0; j < 2; j++)
                    {
                        if (j == 0)
                        {
                            this.string_interpr += this.matrix[i, j];
                        }
                        else
                        {
                            this.string_interpr += " " + this.matrix[i, j];
                        }
                        

                    }

                    if (i != 1)
                    {
                        this.string_interpr += '\n';
                    }

                    
                }

                return this.string_interpr;
            }

            public static Matrix Parse(string str)
            {
                Matrix matrix = new Matrix(new double[2,2]);
                string[] rows = str.Split("\n");
                if (TryParse(str, out matrix))
                {
                    for (int i = 0; i < rows.Length; i ++)
                    {
                        string[] coloumns = rows[i].Split(" ");
                        for (int j = 0; j < coloumns.Length; j ++)
                        {
                            matrix[i, j] = Double.Parse(coloumns[j]);
                        
                        }
                    }

                    return matrix;
                }

                throw new Exception("String is invalid for parse to matrix!");

            }

            public static bool TryParse(string str, out Matrix matrix)
            {
                matrix = new Matrix(new double[2,2]);
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

                for (int i = 0; i < rows.Length; i ++)
                {
                    string[] coloumns = rows[i].Split(" ");
                    for (int j = 0; j < coloumns.Length; j ++)
                    {
                        matrix[i, j] = Double.Parse(coloumns[j]);
                        
                    }
                }

                return true;
            }

            public static Matrix operator -(Matrix m1, Matrix m2)
            {
                Matrix matrix = new Matrix(new double[2,2]);
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
                Matrix matrix = new Matrix(new double[2,2]);
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
                Matrix matrix = new Matrix(new double[2,2]);
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
                Matrix matrix = new Matrix(new double[2,2]);
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
                return m1.Determinant() > m2.Determinant();
            }
            
            public static bool operator <(Matrix m1, Matrix m2)
            {
                return m1.Determinant() < m2.Determinant();
            }
            
            public static bool operator >=(Matrix m1, Matrix m2)
            {
                return m1.Determinant() >= m2.Determinant();
            }
            
            public static bool operator <=(Matrix m1, Matrix m2)
            {
                return m1.Determinant() <= m2.Determinant();
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

                Matrix matrix = new Matrix(new double[2,2]);

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
                    return this.Determinant().CompareTo(compareParMatrix.Determinant());
            }
            
            
            
            public static Matrix operator /(Matrix m1, Matrix m2)
            {

                Matrix matrix = new Matrix(new double[2,2]);
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
        class ListOfMatrix : List<Matrix>
        {
            public void Sort()
            {
                this.Sort(delegate(Matrix x, Matrix y)
                {
                    if (x.Determinant() == null && x.Determinant() == null) return 0;
                    else if (x.Determinant() == null) return -1;
                    else if (y.Determinant() == null) return 1;
                    else return x.Determinant().CompareTo(y.Determinant());
                });
            }
            
            public Matrix Max()
            {
                Matrix max = this[0];
                foreach (var matrix in this)
                {
                    if (matrix.Determinant() > max.Determinant())
                    {
                        max = matrix;
                    }
                }

                return max;
            }
            
            public Matrix Min()
            {
                Matrix min = this[0];
                foreach (var matrix in this)
                {
                    if (matrix.Determinant() < min.Determinant())
                    {
                        min = matrix;
                    }
                }

                return min;
            }

            public Matrix[] ToArray()
            {
                Matrix[] array = new Matrix[this.Count];
                for (int i = 0; i < this.Count; i++)
                {
                    array[i] = this[i];
                }

                return array;
            }

            public string[] ToString()
            {
                string[] array = new string[this.Count];
                for (int i = 0; i < this.Count; i++)
                {
                    array[i] = this[i].ToString();
                }

                return array;

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
                        string data = sr.ReadToEnd();
                        string[] arr = data.Split("\n\n");
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
                        sw.WriteLine(list.ToString());
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
            foreach (Matrix matrix in list)
            {
                if (matrix.Determinant() < n)
                {
                    res.Add(matrix);
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
                Console.WriteLine("Ведите матрицы:");
                StringBuilder sb = new StringBuilder();
                string line = "";
                do
                {
                    line = Console.ReadLine();
                    sb.AppendLine(line);
                } while (line != "-");
                string[] arr = sb.ToString().Split("\n+\n");
                foreach (string matrix in arr)
                {
                    list.Add(Matrix.Parse(matrix));
                }

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
            else
            {
                Console.WriteLine("Некорректные данные!");
                return;
            }

        }
    }
}
using System;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            char[] block = new char[4];
            string text;
            int n, i2, j2, k2, count = 0, prev;

            text = textBox1.Text;


            n = text.Length;
            if (n % 4 == 1)
                text += "\0\0\0";
            else if (n % 4 == 2)
                text += "\0\0";
            else if (n % 4 == 3)
                text += "\0";
           
            
            for (i2 = 0; i2 < text.Length; i2 += 4)
            {
                for (k2 = 0, j2 = i2; k2 < 4 && j2 < i2 + 4; k2++,j2++)
                {
                    block[k2] = text[j2];
                }

             

         
                int[] pt = new int[4];
                int[,] cipher = new int[5,5];
                int[,] mult = new int[5, 5];
                int a, b, c, sum = 0, i, j, k, p, q;
                int[] r = new int[4];
                int[,] key = new int[5, 5]
                    { { 1,1,1,1,1,},
                    { 0,1,1,1,1},
                    { 0,0,1,1,1},
                    { 0,0,0,1,1},
                    { 0,0,0,0,1} };

                int[,] kinv = new int[5, 5]
                    { {1,-1,0,0,0},
                    {0,1,-1,0,0},
                    {0,0,1,-1,0},
                    {0,0,0,1,-1},
                    {0,0,0,0,1} };

                int[,] enc = new int[5, 5];

                char str, ltr;



                for (a = 0; a < 5; a++)
                    for (b = 0; b < 5; b++)
                        sum = sum + key[a,b];
                


                i=block[0];
                r[0]=i-sum-96;

           

                for(k=0,j=1 ; k<3 && j<4 ; k++,j++)
                {
                    p=block[k];
                    q=block[k + 1];
                    r[j]=q-p;
                    
                }//for loop ends


                

                Random rand = new Random();

                for(a=0;a<5;a++)
                {
                    for(b=0;b<5;b++)
                    {
                        if(a==b)
                            cipher[a,b]=0;
                        else if(a==0&&b!=1)
                            cipher[a,b]=0;
                        else if(b==0&&a!=1)
                            cipher[a,b]=0;
                        else if(a==0&&b==1)
                            cipher[a,b]=r[0];
                        else if(a==1&&b==2)
                            cipher[a,b]=r[1];
                        else if(a==2&&b==3)
                            cipher[a,b]=r[2];
                        else if(a==3&&b==4)
                            cipher[a,b]=r[3];
                        else if(b==0&&a==1)
                            cipher[a,b]=r[0];
                        else if(b==1&&a==2)
                            cipher[a,b]=r[1];
                        else if(b==2&&a==3)
                            cipher[a,b]=r[2];
                        else if(b==3&&a==4)
                            cipher[a,b]=r[3];
                        else
                            cipher[a,b]=(rand.Next()%150 + 128);
                    }
                }


               


                for(a=0;a<5;++a)
                {
                    for (b = 0; b < 5; ++b)
                    {
                        mult[a,b] = 0;
                        for (c = 0; c < 5; c++)
                        {
                            mult[a,b] += cipher[a,c] * key[c,b];
                        }
                    }
                }


                richTextBox1.Text += "Output Matrix:\n\n";
                for (a = 0; a < 5; ++a)
                {
                    for (b = 0; b < 5; ++b)
                    {
                        richTextBox1.Text += " " + mult[a, b];
                        if (b == 5 - 1) ;
                    }
                    richTextBox1.Text += "\n";
                }




                for (a=0;a<5;++a)
                {
                    for(b=0;b<5;++b)
                    {
                        enc[a,b]=0;
                        for(c=0;c<5;c++)
                        {
                            enc[a,b]+=mult[a,c]* kinv[c,b];
                        }
                    }
                }


                richTextBox1.Text += "\n Original Matrix:";

                for(a=0;a<5;++a)
                    for(b=0;b<5;++b)
                    {
                        richTextBox1.Text += " " + enc[a,b];
                        if (b == 4)
                            richTextBox1.Text += "\n";
                    }

                int kv = sum + 96;

                for (int i3=0;i3<4;i3++)
                {
                    if (i3==0)
                    {
                        pt[i3] = kv+enc[i3,i3 + 1];
                        ltr = (char)pt[i3];
                        textBox2.Text += ltr;
                    }
                    else{
                       pt[i3]=pt[i3 - 1]+enc[i3,i3 + 1];
                       ltr = (char)pt[i3];
                        textBox2.Text += ltr; 
                    }
                }

                count++;
                richTextBox1.Text += "------------------------";
            }

            richTextBox1.Text += "\n" + count;
        }

    }

}

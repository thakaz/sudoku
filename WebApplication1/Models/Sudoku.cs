using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class Sudoku
    {
        int?[,] tiles = new int?[9,9];

        int?[] tileRows = new int?[9];
        int?[] tileCols = new int?[9];
        int?[] tileBoxes = new int?[9];

        List<int?[]> rowsList = new List<int?[]>();
        List<int?[]> colsList = new List<int?[]>();
        List<int?[]> boxList = new List<int?[]>();

        public Sudoku()
        {
            //初期化
            for (var i = 1; i <= 9; i++)
            {
                rowsList.Add(new int?[9]);
                colsList.Add(new int?[9]);
                boxList.Add(new int?[9]);
            }

            //とりあえず完成させる
            for (var i = 1; i <= 9; i++)
            {
                var temprowList = rowsList[i];
                var randList = Enumerable.Range(1, 9).OrderBy(a => Guid.NewGuid()).ToList();

                for (var j = 1; j <= 9; j++)
                {


                    foreach (int elem in randList)
                    {
                        if (CanSet(i, j, elem))
                        {
                            SetValueToSudoku(i, j, elem);
                            randList.Remove(elem);

                            break; 
                        }
                    }



                }



            } 



        }

        /// <summary>
        /// 値をセット可能か判定
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <param name="elem"></param>
        /// <returns></returns>
        private bool CanSet(int i, int j, int elem)
        {
            //行に存在
            if(Array.IndexOf(rowsList[i],elem)>0) {
                return false;
            }

            //列に存在
            if (Array.IndexOf(colsList[j], elem) > 0) 
            {
                return false;
            }
            //ボックスに存在
            //どのボックスに存在するか変換

            if (Array.IndexOf(boxList[Boxindex(i,j)], elem) >0)
            {
                return false;
            }

            return true;
        }


        private void SetValueToSudoku(int i, int j, int elem)
        {
            tiles[i, j] = elem;
            rowsList[i][j] = elem;
            colsList[j][i] = elem;
            boxList[Boxindex(i, j)][InnerBoxindex(i, j)] = elem;

        }

        private int Boxindex(int i, int j)
        {
            return ((i + 2) / 3) + ((((j + 2) / 3) - 1) * 3);
        }

        private int InnerBoxindex(int i, int j)
        {
            return ((i + 2) % 3) + 1 + ((j + 2) % 3) * 3;
        }

    }
}

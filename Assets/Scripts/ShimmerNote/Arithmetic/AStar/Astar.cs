using System.Collections.Generic;
using UnityEngine;

namespace ShimmerNote
{
    public class AStar : MonoBehaviour
    {
        //地图的长宽
        private int mapWidth = 8;
        private int mapHeight = 6;

        //地图的二维数组
        private Point[,] map;

        void Start()
        {
            InitMap();
            Point start = map[2, 3];
            Point end = map[6, 3];

            FindPath(start, end);
            ShowPath(start, end);
        }

        /// <summary>
        /// 初始化地图
        /// </summary>
        private void InitMap()
        {
            map = new Point[mapWidth, mapHeight];
            for (int y = 0; y < mapHeight; y++)
            {
                for (int x = 0; x < mapWidth; x++)
                {
                    map[x, y] = new Point(x, y);
                }
            }

            map[4, 2].IsWall = true;
            map[4, 3].IsWall = true;
            map[4, 4].IsWall = true;
        }

        /// <summary>
        /// 创建Cube当做地图
        /// </summary>
        private void CreateCube(int x, int y, Color color)
        {
            GameObject go = GameObject.CreatePrimitive(PrimitiveType.Cube);
            go.transform.position = new Vector3(x, y, 0);
            go.GetComponent<Renderer>().material.color = color;
        }

        /// <summary>
        /// 创建Cube当作路标
        /// </summary>
        private void ShowPath(Point start, Point end)
        {
            Point temp = end;
            while (true)
            {
                //Debug.Log(temp.X + "-" + temp.Y);
                Color color = Color.gray;
                if (temp == start)
                {
                    color = Color.green;
                }
                if (temp == end)
                {
                    color = Color.red;
                }
                CreateCube(temp.X, temp.Y, color);
                if (temp.Parent == null)
                    break;
                temp = temp.Parent;
            }
            for (int y = 0; y < mapHeight; y++)
            {
                for (int x = 0; x < mapWidth; x++)
                {
                    if (map[x, y].IsWall)
                    {
                        CreateCube(x, y, Color.blue);
                    }
                }
            }
        }

        int index;

        /// <summary>
        /// 查找路径 A*算法核心逻辑
        /// </summary>
        /// <param name="start">开始点</param>
        /// <param name="end">目标点</param>
        private void FindPath(Point start, Point end)
        {
            index++;
            Debug.Log(index);

            //开启列表
            List<Point> openList = new List<Point>();
            //关闭列表
            List<Point> closeList = new List<Point>();

            openList.Add(start);

            while (openList.Count > 0)
            {
                //获取到列表中F最小的一个参数
                Point point = FindMinFOfPoint(openList);
                //开启列表中移除 加入关闭列表
                openList.Remove(point);
                closeList.Add(point);

                //获得周围的Point列表
                List<Point> surroundPointsList = GetSurroundPoints(point);

                //过滤掉在关闭列表中的Point
                PointsFilter(surroundPointsList, closeList);

                foreach (Point surroundPoint in surroundPointsList)
                {
                    //surroundPoint是否存储在openList中
                    if (openList.IndexOf(surroundPoint) > -1)
                    {
                        //计算当前点的G值
                        float nowG = CalculateG(surroundPoint, point);
                        //当当前的点小于周围点的G值时
                        if (nowG < surroundPoint.G)
                        {
                            //更新父节点和G值的值
                            surroundPoint.UpdateParent(point, nowG);
                        }
                    }
                    else
                    {

                        surroundPoint.Parent = point;

                        //计算当前值 的F值G值
                        CalculateF(surroundPoint, end);

                        //将surroundPoint加入到openList中
                        openList.Add(surroundPoint);
                    }
                }
                //判断一下是否到达了目标点
                if (openList.IndexOf(end) > -1)
                {
                    break;
                }
            }
        }

        /// <summary>
        /// 过滤掉关闭列表中的Point
        /// </summary>
        private void PointsFilter(List<Point> src, List<Point> closePoint)
        {
            foreach (Point p in closePoint)
            {
                if (src.IndexOf(p) > -1)
                {
                    src.Remove(p);
                }
            }
        }

        /// <summary>
        /// 获得周围的point
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        private List<Point> GetSurroundPoints(Point point)
        {
            Point up = null, down = null, left = null, right = null;
            Point lu = null, ld = null, ru = null, rd = null;
            //取得上下左右的Point
            if (point.Y < mapHeight - 1)
            {
                up = map[point.X, point.Y + 1];
            }
            if (point.Y > 0)
            {
                down = map[point.X, point.Y - 1];
            }
            if (point.X > 0)
            {
                left = map[point.X - 1, point.Y];
            }
            if (point.X < mapWidth - 1)
            {
                right = map[point.X + 1, point.Y];
            }
            //取得左上、左下、右上、右下的Point
            if (left != null && up != null)
            {
                lu = map[point.X - 1, point.Y + 1];
            }
            if (left != null && down != null)
            {
                ld = map[point.X - 1, point.Y - 1];
            }
            if (right != null && up != null)
            {
                ru = map[point.X + 1, point.Y + 1];
            }
            if (right != null && down != null)
            {
                rd = map[point.X + 1, point.Y - 1];
            }

            List<Point> pointList = new List<Point>();
            //周围的Point如果不是墙就可以走
            if (up != null && up.IsWall == false)
            {
                pointList.Add(up);
            }
            if (down != null && down.IsWall == false)
            {
                pointList.Add(down);
            }
            if (left != null && left.IsWall == false)
            {
                pointList.Add(left);
            }
            if (right != null && right.IsWall == false)
            {
                pointList.Add(right);
            }

            //两边也不是墙才可以走
            if (lu != null && lu.IsWall == false && left.IsWall == false && up.IsWall == false)
            {
                pointList.Add(lu);
            }
            if (ld != null && ld.IsWall == false && left.IsWall == false && down.IsWall == false)
            {
                pointList.Add(ld);
            }
            if (ru != null && ru.IsWall == false && right.IsWall == false && up.IsWall == false)
            {
                pointList.Add(ru);
            }
            if (rd != null && rd.IsWall == false && right.IsWall == false && down.IsWall == false)
            {
                pointList.Add(rd);
            }
            return pointList;
        }

        /// <summary>
        /// 查找开启列表中的最小F值
        /// </summary>
        /// <param name="openList"></param>
        /// <returns></returns>
        private Point FindMinFOfPoint(List<Point> openList)
        {
            float f = float.MaxValue;
            Point temp = null;
            foreach (Point p in openList)
            {
                if (p.F < f)
                {
                    temp = p;
                    f = p.F;
                }
            }
            return temp;
        }

        /// <summary>
        /// 计算F的值
        /// </summary>
        /// <param name="now">当前位置</param>
        /// <param name="end">目标位置</param>
        private void CalculateF(Point now, Point end)
        {
            //F = G + H
            float h = Mathf.Abs(end.X - now.X) + Mathf.Abs(end.Y - now.Y);
            float g = 0;

            if (now.Parent == null)
            {
                g = 0;
            }
            else
            {
                g = Vector2.Distance(new Vector2(now.X, now.Y), new Vector2(now.Parent.X, now.Parent.Y)) + now.Parent.G;
            }

            float f = g + h;
            now.F = f;
            now.G = g;
            now.H = h;
        }

        /// <summary>
        /// 计算G的值
        /// </summary>
        private float CalculateG(Point now, Point parent)
        {
            return Vector2.Distance(new Vector2(now.X, now.Y), new Vector2(parent.X, parent.Y)) + parent.G;
        }
    }
}
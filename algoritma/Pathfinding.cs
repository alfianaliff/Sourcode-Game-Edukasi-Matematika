using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour {

    Grid GridReference; //untuk membuat object
    public Transform StartPosition; 
    public Transform TargetPosition;
    
    private void Awake()
    {
        GridReference = GetComponent<Grid>();
    }

    private void Update()
    {
        FindPath(StartPosition.position, TargetPosition.position); //menemukan jalan menuju tujuan
    }
    
    void FindPath(Vector3 a_StartPos, Vector3 a_TargetPos) 
    {
        Node StartNode = GridReference.NodeFromWorldPoint(a_StartPos); //sebagai posisi npc
        Node TargetNode = GridReference.NodeFromWorldPoint(a_TargetPos); //node dari posisi player ketika diperiksa maka metode mencari jalur berhenti dan membentuk jalur pathfinding
        List<Node> OpenList = new List<Node>(); 
        HashSet<Node> ClosedList = new HashSet<Node>(); 

        OpenList.Add(StartNode);
        
        while(OpenList.Count > 0) //jika openlist masih tersedia maka proses untuk menemukan jalur akan terus berjalan
        {
            Node CurrentNode = OpenList[0];
            for(int i = 1; i < OpenList.Count; i++) 
            {
                if (OpenList[i].FCost < CurrentNode.FCost || OpenList[i].FCost == CurrentNode.FCost && OpenList[i].ihCost < CurrentNode.ihCost)
                {
                    CurrentNode = OpenList[i];
                }
            }
        
            OpenList.Remove(CurrentNode);
            ClosedList.Add(CurrentNode);

            if (CurrentNode == TargetNode)
            {
                GetFinalPath(StartNode, TargetNode);
            }

            foreach (Node NeighborNode in GridReference.GetNeighboringNodes(CurrentNode)) //sebuah rumus mencari jalur terpendek menggunakan a-star
            {
                if (!NeighborNode.bIsWall || ClosedList.Contains(NeighborNode))
                {
                    continue;
                }
                int MoveCost = CurrentNode.igCost + GetManhattenDistance(CurrentNode, NeighborNode);// untuk menghitung kecepatan
            
                if (MoveCost < NeighborNode.igCost || !OpenList.Contains(NeighborNode))
                {
                    NeighborNode.igCost = MoveCost; //cost dari titik awal ketitik yang diperiksa
                    NeighborNode.ihCost = GetManhattenDistance(NeighborNode, TargetNode); //cost titik yang diperiksa terhadap target
                    NeighborNode.ParentNode = CurrentNode;

                    if(!OpenList.Contains(NeighborNode))
                    {
                        OpenList.Add(NeighborNode);
                    }
                }
            }
    }
}


    void GetFinalPath(Node a_StartingNode, Node a_EndNode)
    {
        List<Node> FinalPath = new List<Node>();
        Node CurrentNode = a_EndNode;

        while(CurrentNode != a_StartingNode)
        {
            FinalPath.Add(CurrentNode);
            CurrentNode =  CurrentNode.ParentNode;
        }

        FinalPath.Reverse(); 

        GridReference.FinalPath = FinalPath; 
    }

    int GetManhattenDistance(Node a_nodeA, Node a_nodeB) //untuk mendapatkan jarak dengan parameter node a dan node b
    {
        int ix = Mathf.Abs(a_nodeA.iGridX - a_nodeB.iGridX);
        int iy = Mathf.Abs(a_nodeA.iGridY - a_nodeB.iGridY);
        

        return ix + iy; 
    }

} 
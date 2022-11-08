using UnityEngine;
using System.IO;
using System;

public class readFile : MonoBehaviour
{

    public Mesh createdObject1;
    public Mesh createdObject2;

    public Material Material1;
    public Material Material2;

    private GameObject []objects1;
    private GameObject[] objects2;
    
    private int objectCount1;
    private int objectCount2;



    void ReadAndCreateObjects(string path, int objectNum )
    {
        int point_count = 0;
        double x = 0, y = 0, z = 0;
        
        StreamReader reader = new StreamReader(path);
        string readed = reader.ReadLine();

        if (readed != null)
            point_count = int.Parse(readed);
        
        char delimiterChar =  ' ';
        int k = 0;

        if(objectNum == 1){
            
            objects1 = new GameObject[point_count];
            objectCount1 = 0;
            
        }
        else{
            
            objects2 = new GameObject[point_count];
            objectCount2 = 0;
            
        }

        for (int i = 1; i < point_count + 1; i++){
            
            readed = reader.ReadLine();
            
            var gameObject = new GameObject("OBJECT " + objectNum+ " : " + i);
            var meshFilter = gameObject.AddComponent<MeshFilter>();
            
            gameObject.AddComponent<MeshRenderer>();
            
            if(objectNum == 1){
                
                meshFilter.sharedMesh = createdObject1;
                gameObject.GetComponent<MeshRenderer>().material = Material1;
                
            }
            else if(objectNum == 2) {
                
                meshFilter.sharedMesh = createdObject2;
                gameObject.GetComponent<MeshRenderer>().material = Material2;
                
            }

            string[] points = readed.Split(delimiterChar);
            k = 0;
            
            foreach (string point in points){ 
                if (k == 0)
                {
                    x = Convert.ToDouble(point);
                    Debug.Log("X:");
                    Debug.Log(x);
                }
                else if (k == 1)
                {
                    y = Convert.ToDouble(point);
                }
                else if (k == 2)
                {
                    z = Convert.ToDouble(point);
                }
                k++;
            }
            
            Vector3 temp = new Vector3((float)x, (float)y, (float)z);
           
            gameObject.transform.position += temp;
            gameObject.transform.localScale += new Vector3(-0.75f, -0.75f, -0.75f);

            if (objectNum == 1)
                objects1[objectCount1++] = gameObject;
            
            else if (objectNum == 2)
                objects2[objectCount2++] = gameObject;
            
            
        }

        reader.Close();
    }

    void Start(){
        string path1 = "Assets/Resources/file1.txt";
        string path2 = "Assets/Resources/file2.txt";
        ReadAndCreateObjects(path1,1 );
        ReadAndCreateObjects(path2, 2);
    }

  
}
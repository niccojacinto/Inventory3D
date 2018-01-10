using UnityEngine;

[System.Serializable]
public struct IntVector3  {

    public Vector3 vector3;
    public int x { get { return Mathf.RoundToInt(vector3.x); } }
    public int y { get { return Mathf.RoundToInt(vector3.y); } }
    public int z { get { return Mathf.RoundToInt(vector3.z); } }

    public IntVector3(Vector3 v3) {
        vector3 = v3.ToIntVector3();
    }

    public IntVector3(int x, int y, int z) {
        vector3 = new Vector3(x,y,z);
    }

    public IntVector3(float x, float y, float z) {
        vector3 = new Vector3(x, y, z);
    }

    public Vector3 GetIntVector() {
        return new Vector3(x,y,z);
    }
}

public static class IntVector3Extension {
    public static Vector3 ToIntVector3 (this Vector3 v3) {
        return new Vector3 (Mathf.RoundToInt(v3.x), Mathf.RoundToInt(v3.y), (Mathf.RoundToInt(v3.z)));
    }
}

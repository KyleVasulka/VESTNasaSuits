using UnityEngine;

public class Model3dARC
{

    string link;
    Vector3 position;
    Quaternion rotation;
    Vector3 scale;

    public void SetLink(string newlink)
    {
        link = newlink;

    }
    public void SetPos(Vector3 newPos)
    {
        position = newPos;
    }
    public string GetLink()
    {
        return link;
    }
    public Vector3 GetPos()
    {
        return position;
    }
    public void SetRotation(Quaternion rotation)
    {
        this.rotation = rotation;
    }
    public Quaternion GetRotation()
    {
        return rotation;
    }
    public void SetScale(Vector3 scale)
    {
        this.scale = scale;
    }
    public Vector3 GetScale()
    {
        return scale;
    }
}

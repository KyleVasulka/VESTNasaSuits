using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class IU
{

    public string iu_id, iu_link, iu_type, iau_id, iu_position, iu_rotation, iu_scale, iu_space;

    public Vector3 GetPosition()
    {
        string[] ordinates = iu_position.Split(',');
        float x = float.Parse(ordinates[0], CultureInfo.InvariantCulture.NumberFormat);
        float y = float.Parse(ordinates[1], CultureInfo.InvariantCulture.NumberFormat);
        float z = float.Parse(ordinates[2], CultureInfo.InvariantCulture.NumberFormat);
        return new Vector3(x,y,z);
    }

    public Quaternion GetRotation()
    {
        string[] rotations = iu_rotation.Split(',');
        float x = float.Parse(rotations[0], CultureInfo.InvariantCulture.NumberFormat);
        float y = float.Parse(rotations[1], CultureInfo.InvariantCulture.NumberFormat);
        float z = float.Parse(rotations[2], CultureInfo.InvariantCulture.NumberFormat);
        return Quaternion.Euler(x, y, z);
    }

    public Vector3 GetScale()
    {
        string[] scales = iu_scale.Split(',');
        float x = float.Parse(scales[0], CultureInfo.InvariantCulture.NumberFormat);
        float y = float.Parse(scales[1], CultureInfo.InvariantCulture.NumberFormat);
        float z = float.Parse(scales[2], CultureInfo.InvariantCulture.NumberFormat);
        return new Vector3(x, y, z);
    }

}

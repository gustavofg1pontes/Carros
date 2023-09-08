using System;
using System.Numerics;

namespace RaceIF;

public static class VectorUtils
{

    public static Vector2 AddAngles(Vector2 vector2, float angle)
    {
        return ChangeAngle(vector2, AddAngles(AngleFromVector(vector2), angle));
    }

    public static Vector2 SubtractAngles(Vector2 vector2, float angle)
    {
        return ChangeAngle(vector2, SubtractAngles(AngleFromVector(vector2), angle));
    }

    public static Vector2 ChangeAngle(Vector2 vector, float newAngleDegrees)
    {
        float magnitude = vector.Length(); // Obtém a magnitude do vetor

        // Converte o novo ângulo para radianos
        float newAngleRadians = newAngleDegrees * (float)Math.PI / 180.0f;

        // Calcula as novas componentes x e y do vetor usando trigonometria
        float x = magnitude * (float)Math.Cos(newAngleRadians);
        float y = magnitude * (float)Math.Sin(newAngleRadians);

        return new Vector2(x, y);
    }

    public static float AngleFromVector(Vector2 vector)
    {
        double angleRadians = Math.Atan2(vector.Y, vector.X);
        double angleDegrees = angleRadians * (180.0 / Math.PI);

        return NormalizeAngle((float)angleDegrees);
    }

    public static float GetOppositeAngle(this Vector2 vector)
    {
        // Calculate the angle of the vector in radians
        float angle = (float)Math.Atan2(vector.Y, vector.X);

        // Calculate the opposite angle by adding or subtracting π radians (180 degrees)
        float oppositeAngle = angle + (float)Math.PI;

        // Ensure the angle is within the range [0, 2π) radians
        if (oppositeAngle >= 2 * Math.PI)
        {
            oppositeAngle -= (float)(2 * Math.PI);
        }
        else if (oppositeAngle < 0)
        {
            oppositeAngle += (float)(2 * Math.PI);
        }

        return NormalizeAngle((float)(oppositeAngle  * 180 / Math.PI));
    }

    public static Vector2 VectorFromAngle(float angleDegrees)
    {
        // Converter o ângulo de graus para radianos
        float angleRadians = (float)(angleDegrees * Math.PI / 180.0);

        // Calcular as componentes x e y do vetor
        float x = (float)Math.Cos(angleRadians);
        float y = (float)Math.Sin(angleRadians);

        return new Vector2(x, y);
    }

    public static Vector2 CreateVectorFromScalarAndAngle(float magnitude, float angleDegrees)
    {
        // Converte o ângulo para radianos
        float angleRadians = angleDegrees * (float)Math.PI / 180.0f;

        // Calcula as componentes x e y do vetor usando trigonometria
        float x = magnitude * (float)Math.Cos(angleRadians);
        float y = magnitude * (float)Math.Sin(angleRadians);

        return new Vector2(x, y);
    }

    public static float CalculateMagnitude(Vector2 vector)
    {
        // Calcula a magnitude do vetor usando o teorema de Pitágoras
        float magnitude = (float)Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y);
        return magnitude;
    }

    public static float NormalizeAngle(float angle)
    {
        // Normaliza o ângulo para o intervalo de 0 a 360 graus
        while (angle < 0)
            angle += 360;

        return angle % 360;
    }

    public static float AddAngles(float angle1, float angle2)
    {
        return NormalizeAngle(angle1 + angle2);
    }

    public static float SubtractAngles(float angle1, float angle2)
    {
        return NormalizeAngle(angle1 - angle2);
    }

    public static Vector2 AddAngles(Vector2 vector1, Vector2 vector2)
    {
        // Converte os vetores em ângulos e realiza a adição

        float angle1 = AngleFromVector(vector1);
        float angle2 = AngleFromVector(vector2);

        float resultAngle = AddAngles(angle1, angle2);

        return VectorFromAngle(resultAngle);
    }

    public static Vector2 SubtractAngles(Vector2 vector1, Vector2 vector2)
    {
        // Converte os vetores em ângulos e realiza a subtração
        float angle1 = AngleFromVector(vector1);
        float angle2 = AngleFromVector(vector2);

        float resultAngle = SubtractAngles(angle1, angle2);

        return VectorFromAngle(resultAngle);
    }

    public static bool HasChangedDirection(Vector2 vector1, Vector2 vector2)
    {
        double dotProduct = 0;

        dotProduct += Vector2.Dot(vector1, vector2);

        return dotProduct < 0;
    }
}
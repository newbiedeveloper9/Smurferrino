using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Darc_Euphoria.Euphoric;
using static Darc_Euphoria.Euphoric.Structures;

namespace Smurferrino.Business.Helpers
{
    public static class MathFuncs
        {
            public static Structures.Vector2 CalcAngle(Structures.Vector3 src, Structures.Vector3 dist)
            {
                Structures.Vector3 delta = new Structures.Vector3()
                {
                    x = dist.x - src.x,
                    y = dist.y - src.y,
                    z = dist.z - src.z,
                };

                float magn = (float)Math.Sqrt(
                    (delta.x * delta.x) +
                    (delta.y * delta.y) +
                    (delta.z * delta.z)
                );

                Structures.Vector2 returnAngle = new Structures.Vector2()
                {
                    x = (float)(Math.Atan2(delta.y, delta.x) * (180f / 3.14f)),
                    y = (float)(-Math.Atan2(delta.z, magn) * (180f / 3.14f)),
                };

                return returnAngle;
            }

            public static double CalcFov(Structures.Vector2 src, Structures.Vector2 dist)
            {
                double i = Math.Sqrt(
                    ((dist.x - src.x) * (dist.x - src.x)) +
                    ((dist.y - src.y) * (dist.y - src.y))
                );
                return i;
            }

            public static string GetRankName(this int id)
            {
                switch (id)
                {
                    case 0:
                        return "Unranked";
                    case 1:
                        return "Silver 1";
                    case 2:
                        return "Silver 2";
                    case 3:
                        return "Silver 3";
                    case 4:
                        return "Silver 4";
                    case 5:
                        return "Silver Elite";
                    case 6:
                        return "Silver Elite Master";
                    case 7:
                        return "Gold Nova 1";
                    case 8:
                        return "Gold Nova 2";
                    case 9:
                        return "Gold Nova 3";
                    case 10:
                        return "Gold Nova Master";
                    case 11:
                        return "Master Guardian 1";
                    case 12:
                        return "Master Guardian 2";
                    case 13:
                        return "Master Guardian Elite";
                    case 14:
                        return "Distinguished Master Guardian";
                    case 15:
                        return "Legendary Eagle";
                    case 16:
                        return "Legendary Eagle Master";
                    case 17:
                        return "Supreme Master First Class";
                    case 18:
                        return "Global Elite";
                }

                return string.Empty;
            }

            public static float VectorDistance(Structures.Vector3 src, Structures.Vector3 dist, bool noZ = false)
            {
                if (!noZ)
                {
                    double distance = Math.Sqrt(
                    ((dist.x - src.x) * (dist.x - src.x)) +
                    ((dist.y - src.y) * (dist.y - src.y)) +
                    ((dist.z - src.z) * (dist.z - src.z))
                    );
                    distance = Math.Round(distance, 4);
                    return (float)distance;
                }
                else
                {
                    double distance = Math.Sqrt(
                       ((dist.x - src.x) * (dist.x - src.x)) +
                       ((dist.y - src.y) * (dist.y - src.y))
                       );

                    distance = Math.Round(distance, 4);
                    return (float)distance;
                }
            }

            public static float VectorDistance(Structures.Vector2 src, Structures.Vector2 dist)
            {
                double distance = Math.Sqrt(
                    (dist.x - src.x) * (dist.x - src.x) +
                    (dist.y - src.y) * (dist.y - src.y)
                 );

                distance = Math.Round(distance, 4);
                return (float)distance;
            }

            public static Structures.Vector2 NormalizeAngle(this Structures.Vector2 angle)
            {
                while (0f > angle.x || angle.x > 360f)
                {
                    if (angle.x < 0f) angle.x += 360.0f;
                    if (angle.x > 360f) angle.x -= 360.0f;
                }
                return angle;
            }

            public static Structures.Vector2 ClampAngle(this Structures.Vector2 angle)
            {
                while (angle.x > 180f)
                    angle.x -= 360f;

                while (angle.x < -180f)
                    angle.x += 360f;

                while (angle.y > 89f)
                    angle.y -= (angle.y - 89) * 2;

                while (angle.y < -89f)
                    angle.y += (-89 - angle.y) * 2;


                if (angle.x > 180f)
                    return Global.LocalPlayer.ViewAngle;

                if (angle.x < -180f)
                    return Global.LocalPlayer.ViewAngle; //This is to FORCE clamped angles 
                                            //even if i failed it somehow
                if (angle.y > 89f)
                    return Global.LocalPlayer.ViewAngle;

                if (angle.y < -89f)
                    return Global.LocalPlayer.ViewAngle;


                return angle;
            }

            public static Structures.Vector3 ToVector3(this Structures.Vector2 angle)
            {
                return new Structures.Vector3(angle.y, angle.x, 0);
            }

            public static Color HealthToColor(this int id)
            {
                int i = id;
                if (255 - (int)(id * 2.55) < 0)
                    i = 0;

                if (255 - (int)(id * 2.55) > 255)
                    i = 255;

                if ((int)(id * 2.55) > 255)
                    i = 255;

                if ((int)(id * 2.55) < 0)
                    i = 0;

                return Color.FromArgb(255, 255 - (int)(i * 2.55), (int)(i * 2.55), 80);
            }

            public static Color BombToColor(this float id)
            {
                float i = id;
                if (255 - (int)(id * 6.375) < 0)
                    i = 0;

                if (255 - (int)(id * 6.375) > 255)
                    i = 255;

                if ((int)(id * 6.375) > 255)
                    i = 255;

                if ((int)(id * 6.375) < 0)
                    i = 0;

                return Color.FromArgb(255, 255 - (int)(i * 6.375), (int)(i * 6.375), 80);
            }

            public static Structures.Vector2 ToVector2(this Structures.Vector3 angle)
            {
                return new Structures.Vector2(angle.x, angle.y);
            }
        }
    }

    //https://github.com/DarcPhansea/Darc-Euphoria/blob/8b461aa8b6e7aa168f3e035dfa5bf44aafb64b7b/Darc%20Euphoria/Euphoric/Math.cs
    //Sorry for copy-paste, my math sucks
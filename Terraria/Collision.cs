﻿namespace Terraria
{
    using Microsoft.Xna.Framework;
    using System;
    using System.Runtime.InteropServices;

    public class Collision
    {
        public static bool down;
        public static bool up;

        public static Vector2 AnyCollision(Vector2 Position, Vector2 Velocity, int Width, int Height)
        {
            Vector2 vector = Velocity;
            Vector2 vector2 = Velocity;
            Vector2 vector4 = Position + Velocity;
            Vector2 vector5 = Position;
            int num = ((int) (Position.X / 16f)) - 1;
            int maxTilesX = ((int) ((Position.X + Width) / 16f)) + 2;
            int num3 = ((int) (Position.Y / 16f)) - 1;
            int maxTilesY = ((int) ((Position.Y + Height) / 16f)) + 2;
            int num5 = -1;
            int num6 = -1;
            int num7 = -1;
            int num8 = -1;
            if (num < 0)
            {
                num = 0;
            }
            if (maxTilesX > Main.maxTilesX)
            {
                maxTilesX = Main.maxTilesX;
            }
            if (num3 < 0)
            {
                num3 = 0;
            }
            if (maxTilesY > Main.maxTilesY)
            {
                maxTilesY = Main.maxTilesY;
            }
            for (int i = num; i < maxTilesX; i++)
            {
                for (int j = num3; j < maxTilesY; j++)
                {
                    if ((Main.tile[i, j] != null) && Main.tile[i, j].active)
                    {
                        Vector2 vector3;
                        vector3.X = i * 0x10;
                        vector3.Y = j * 0x10;
                        if ((((vector4.X + Width) > vector3.X) && (vector4.X < (vector3.X + 16f))) && (((vector4.Y + Height) > vector3.Y) && (vector4.Y < (vector3.Y + 16f))))
                        {
                            if ((vector5.Y + Height) <= vector3.Y)
                            {
                                num7 = i;
                                num8 = j;
                                if (num7 != num5)
                                {
                                    vector.Y = vector3.Y - (vector5.Y + Height);
                                }
                            }
                            else if (((vector5.X + Width) <= vector3.X) && !Main.tileSolidTop[Main.tile[i, j].type])
                            {
                                num5 = i;
                                num6 = j;
                                if (num6 != num8)
                                {
                                    vector.X = vector3.X - (vector5.X + Width);
                                }
                                if (num7 == num5)
                                {
                                    vector.Y = vector2.Y;
                                }
                            }
                            else if ((vector5.X >= (vector3.X + 16f)) && !Main.tileSolidTop[Main.tile[i, j].type])
                            {
                                num5 = i;
                                num6 = j;
                                if (num6 != num8)
                                {
                                    vector.X = (vector3.X + 16f) - vector5.X;
                                }
                                if (num7 == num5)
                                {
                                    vector.Y = vector2.Y;
                                }
                            }
                            else if ((vector5.Y >= (vector3.Y + 16f)) && !Main.tileSolidTop[Main.tile[i, j].type])
                            {
                                num7 = i;
                                num8 = j;
                                vector.Y = ((vector3.Y + 16f) - vector5.Y) + 0.01f;
                                if (num8 == num6)
                                {
                                    vector.X = vector2.X + 0.01f;
                                }
                            }
                        }
                    }
                }
            }
            return vector;
        }

        public static bool CanHit(Vector2 Position1, int Width1, int Height1, Vector2 Position2, int Width2, int Height2)
        {
            int num = (int) ((Position1.X + (Width1 / 2)) / 16f);
            int num2 = (int) ((Position1.Y + (Height1 / 2)) / 16f);
            int num3 = (int) ((Position2.X + (Width2 / 2)) / 16f);
            int num4 = (int) ((Position2.Y + (Height2 / 2)) / 16f);
            try
            {
                do
                {
                    int num5 = Math.Abs((int) (num - num3));
                    int num6 = Math.Abs((int) (num2 - num4));
                    if ((num == num3) && (num2 == num4))
                    {
                        return true;
                    }
                    if (num5 > num6)
                    {
                        if (num < num3)
                        {
                            num++;
                        }
                        else
                        {
                            num--;
                        }
                        if (Main.tile[num, num2 - 1] == null)
                        {
                            Main.tile[num, num2 - 1] = new Tile();
                        }
                        if (Main.tile[num, num2 + 1] == null)
                        {
                            Main.tile[num, num2 + 1] = new Tile();
                        }
                        if (((Main.tile[num, num2 - 1].active && Main.tileSolid[Main.tile[num, num2 - 1].type]) && (!Main.tileSolidTop[Main.tile[num, num2 - 1].type] && Main.tile[num, num2 + 1].active)) && (Main.tileSolid[Main.tile[num, num2 + 1].type] && !Main.tileSolidTop[Main.tile[num, num2 + 1].type]))
                        {
                            return false;
                        }
                    }
                    else
                    {
                        if (num2 < num4)
                        {
                            num2++;
                        }
                        else
                        {
                            num2--;
                        }
                        if (Main.tile[num - 1, num2] == null)
                        {
                            Main.tile[num - 1, num2] = new Tile();
                        }
                        if (Main.tile[num + 1, num2] == null)
                        {
                            Main.tile[num + 1, num2] = new Tile();
                        }
                        if (((Main.tile[num - 1, num2].active && Main.tileSolid[Main.tile[num - 1, num2].type]) && (!Main.tileSolidTop[Main.tile[num - 1, num2].type] && Main.tile[num + 1, num2].active)) && (Main.tileSolid[Main.tile[num + 1, num2].type] && !Main.tileSolidTop[Main.tile[num + 1, num2].type]))
                        {
                            return false;
                        }
                    }
                    if (Main.tile[num, num2] == null)
                    {
                        Main.tile[num, num2] = new Tile();
                    }
                }
                while ((!Main.tile[num, num2].active || !Main.tileSolid[Main.tile[num, num2].type]) || Main.tileSolidTop[Main.tile[num, num2].type]);
                return false;
            }
            catch
            {
                return false;
            }
        }

        public static bool DrownCollision(Vector2 Position, int Width, int Height, float gravDir = -1f)
        {
            Vector2 vector2 = new Vector2(Position.X + (Width / 2), Position.Y + (Height / 2));
            int num = 10;
            int num2 = 12;
            if (num > Width)
            {
                num = Width;
            }
            if (num2 > Height)
            {
                num2 = Height;
            }
            vector2 = new Vector2(vector2.X - (num / 2), Position.Y + -2f);
            if (gravDir == -1f)
            {
                vector2.Y += (Height / 2) - 6;
            }
            int num3 = ((int) (Position.X / 16f)) - 1;
            int maxTilesX = ((int) ((Position.X + Width) / 16f)) + 2;
            int num5 = ((int) (Position.Y / 16f)) - 1;
            int maxTilesY = ((int) ((Position.Y + Height) / 16f)) + 2;
            if (num3 < 0)
            {
                num3 = 0;
            }
            if (maxTilesX > Main.maxTilesX)
            {
                maxTilesX = Main.maxTilesX;
            }
            if (num5 < 0)
            {
                num5 = 0;
            }
            if (maxTilesY > Main.maxTilesY)
            {
                maxTilesY = Main.maxTilesY;
            }
            for (int i = num3; i < maxTilesX; i++)
            {
                for (int j = num5; j < maxTilesY; j++)
                {
                    if ((Main.tile[i, j] != null) && (Main.tile[i, j].liquid > 0))
                    {
                        Vector2 vector;
                        vector.X = i * 0x10;
                        vector.Y = j * 0x10;
                        int num9 = 0x10;
                        float num10 = 0x100 - Main.tile[i, j].liquid;
                        num10 /= 32f;
                        vector.Y += num10 * 2f;
                        num9 -= (int) (num10 * 2f);
                        if ((((vector2.X + num) > vector.X) && (vector2.X < (vector.X + 16f))) && (((vector2.Y + num2) > vector.Y) && (vector2.Y < (vector.Y + num9))))
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        public static bool EmptyTile(int i, int j, bool ignoreTiles = false)
        {
            Rectangle rectangle = new Rectangle(i * 0x10, j * 0x10, 0x10, 0x10);
            if (Main.tile[i, j].active && !ignoreTiles)
            {
                return false;
            }
            for (int k = 0; k < 0xff; k++)
            {
                if (Main.player[k].active && rectangle.Intersects(new Rectangle((int) Main.player[k].position.X, (int) Main.player[k].position.Y, Main.player[k].width, Main.player[k].height)))
                {
                    return false;
                }
            }
            for (int m = 0; m < 200; m++)
            {
                if (Main.npc[m].active && rectangle.Intersects(new Rectangle((int) Main.npc[m].position.X, (int) Main.npc[m].position.Y, Main.npc[m].width, Main.npc[m].height)))
                {
                    return false;
                }
            }
            return true;
        }

        public static void HitTiles(Vector2 Position, Vector2 Velocity, int Width, int Height)
        {
            Vector2 vector2 = Position + Velocity;
            int num = ((int) (Position.X / 16f)) - 1;
            int maxTilesX = ((int) ((Position.X + Width) / 16f)) + 2;
            int num3 = ((int) (Position.Y / 16f)) - 1;
            int maxTilesY = ((int) ((Position.Y + Height) / 16f)) + 2;
            if (num < 0)
            {
                num = 0;
            }
            if (maxTilesX > Main.maxTilesX)
            {
                maxTilesX = Main.maxTilesX;
            }
            if (num3 < 0)
            {
                num3 = 0;
            }
            if (maxTilesY > Main.maxTilesY)
            {
                maxTilesY = Main.maxTilesY;
            }
            for (int i = num; i < maxTilesX; i++)
            {
                for (int j = num3; j < maxTilesY; j++)
                {
                    if (((Main.tile[i, j] != null) && Main.tile[i, j].active) && (Main.tileSolid[Main.tile[i, j].type] || (Main.tileSolidTop[Main.tile[i, j].type] && (Main.tile[i, j].frameY == 0))))
                    {
                        Vector2 vector;
                        vector.X = i * 0x10;
                        vector.Y = j * 0x10;
                        if ((((vector2.X + Width) >= vector.X) && (vector2.X <= (vector.X + 16f))) && (((vector2.Y + Height) >= vector.Y) && (vector2.Y <= (vector.Y + 16f))))
                        {
                            WorldGen.KillTile(i, j, true, true, false);
                        }
                    }
                }
            }
        }

        public static Vector2 HurtTiles(Vector2 Position, Vector2 Velocity, int Width, int Height, bool fireImmune = false)
        {
            Vector2 vector2 = Position;
            int num = ((int) (Position.X / 16f)) - 1;
            int maxTilesX = ((int) ((Position.X + Width) / 16f)) + 2;
            int num3 = ((int) (Position.Y / 16f)) - 1;
            int maxTilesY = ((int) ((Position.Y + Height) / 16f)) + 2;
            if (num < 0)
            {
                num = 0;
            }
            if (maxTilesX > Main.maxTilesX)
            {
                maxTilesX = Main.maxTilesX;
            }
            if (num3 < 0)
            {
                num3 = 0;
            }
            if (maxTilesY > Main.maxTilesY)
            {
                maxTilesY = Main.maxTilesY;
            }
            for (int i = num; i < maxTilesX; i++)
            {
                for (int j = num3; j < maxTilesY; j++)
                {
                    Vector2 vector;
                    if (((Main.tile[i, j] == null) || !Main.tile[i, j].active) || ((((Main.tile[i, j].type != 0x20) && (Main.tile[i, j].type != 0x25)) && ((Main.tile[i, j].type != 0x30) && (Main.tile[i, j].type != 0x35))) && ((((Main.tile[i, j].type != 0x39) && (Main.tile[i, j].type != 0x3a)) && ((Main.tile[i, j].type != 0x45) && (Main.tile[i, j].type != 0x4c))) && (((Main.tile[i, j].type != 0x70) && (Main.tile[i, j].type != 0x74)) && (Main.tile[i, j].type != 0x7b)))))
                    {
                        continue;
                    }
                    vector.X = i * 0x10;
                    vector.Y = j * 0x10;
                    int num7 = 0;
                    int type = Main.tile[i, j].type;
                    if (((type == 0x20) || (type == 0x45)) || (type == 80))
                    {
                        if ((((vector2.X + Width) <= vector.X) || (vector2.X >= (vector.X + 16f))) || (((vector2.Y + Height) <= vector.Y) || (vector2.Y >= (vector.Y + 16.01))))
                        {
                            continue;
                        }
                        int num9 = 1;
                        if ((vector2.X + (Width / 2)) < (vector.X + 8f))
                        {
                            num9 = -1;
                        }
                        num7 = 10;
                        switch (type)
                        {
                            case 0x45:
                                num7 = 0x11;
                                break;

                            case 80:
                                num7 = 6;
                                break;
                        }
                        switch (type)
                        {
                            case 0x20:
                            case 0x45:
                                WorldGen.KillTile(i, j, false, false, false);
                                if (((Main.netMode == 1) && !Main.tile[i, j].active) && (Main.netMode == 1))
                                {
                                    NetMessage.SendData(0x11, -1, -1, "", 4, (float) i, (float) j, 0f, 0);
                                }
                                break;
                        }
                        return new Vector2((float) num9, (float) num7);
                    }
                    if (((type == 0x35) || (type == 0x70)) || ((type == 0x74) || (type == 0x7b)))
                    {
                        if (((((vector2.X + Width) - 2f) >= vector.X) && ((vector2.X + 2f) <= (vector.X + 16f))) && ((((vector2.Y + Height) - 2f) >= vector.Y) && ((vector2.Y + 2f) <= (vector.Y + 16f))))
                        {
                            int num10 = 1;
                            if ((vector2.X + (Width / 2)) < (vector.X + 8f))
                            {
                                num10 = -1;
                            }
                            num7 = 20;
                            return new Vector2((float) num10, (float) num7);
                        }
                    }
                    else if ((((vector2.X + Width) >= vector.X) && (vector2.X <= (vector.X + 16f))) && (((vector2.Y + Height) >= vector.Y) && (vector2.Y <= (vector.Y + 16.01))))
                    {
                        int num11 = 1;
                        if ((vector2.X + (Width / 2)) < (vector.X + 8f))
                        {
                            num11 = -1;
                        }
                        if (!fireImmune && (((type == 0x25) || (type == 0x3a)) || (type == 0x4c)))
                        {
                            num7 = 20;
                        }
                        if (type == 0x30)
                        {
                            num7 = 40;
                        }
                        return new Vector2((float) num11, (float) num7);
                    }
                }
            }
            return new Vector2();
        }

        public static bool LavaCollision(Vector2 Position, int Width, int Height)
        {
            int num = Height;
            int num2 = ((int) (Position.X / 16f)) - 1;
            int maxTilesX = ((int) ((Position.X + Width) / 16f)) + 2;
            int num4 = ((int) (Position.Y / 16f)) - 1;
            int maxTilesY = ((int) ((Position.Y + Height) / 16f)) + 2;
            if (num2 < 0)
            {
                num2 = 0;
            }
            if (maxTilesX > Main.maxTilesX)
            {
                maxTilesX = Main.maxTilesX;
            }
            if (num4 < 0)
            {
                num4 = 0;
            }
            if (maxTilesY > Main.maxTilesY)
            {
                maxTilesY = Main.maxTilesY;
            }
            for (int i = num2; i < maxTilesX; i++)
            {
                for (int j = num4; j < maxTilesY; j++)
                {
                    if (((Main.tile[i, j] != null) && (Main.tile[i, j].liquid > 0)) && Main.tile[i, j].lava)
                    {
                        Vector2 vector;
                        vector.X = i * 0x10;
                        vector.Y = j * 0x10;
                        int num8 = 0x10;
                        float num9 = 0x100 - Main.tile[i, j].liquid;
                        num9 /= 32f;
                        vector.Y += num9 * 2f;
                        num8 -= (int) (num9 * 2f);
                        if ((((Position.X + Width) > vector.X) && (Position.X < (vector.X + 16f))) && (((Position.Y + num) > vector.Y) && (Position.Y < (vector.Y + num8))))
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        public static bool SolidCollision(Vector2 Position, int Width, int Height)
        {
            int num = ((int) (Position.X / 16f)) - 1;
            int maxTilesX = ((int) ((Position.X + Width) / 16f)) + 2;
            int num3 = ((int) (Position.Y / 16f)) - 1;
            int maxTilesY = ((int) ((Position.Y + Height) / 16f)) + 2;
            if (num < 0)
            {
                num = 0;
            }
            if (maxTilesX > Main.maxTilesX)
            {
                maxTilesX = Main.maxTilesX;
            }
            if (num3 < 0)
            {
                num3 = 0;
            }
            if (maxTilesY > Main.maxTilesY)
            {
                maxTilesY = Main.maxTilesY;
            }
            for (int i = num; i < maxTilesX; i++)
            {
                for (int j = num3; j < maxTilesY; j++)
                {
                    if (((Main.tile[i, j] != null) && Main.tile[i, j].active) && (Main.tileSolid[Main.tile[i, j].type] && !Main.tileSolidTop[Main.tile[i, j].type]))
                    {
                        Vector2 vector;
                        vector.X = i * 0x10;
                        vector.Y = j * 0x10;
                        if ((((Position.X + Width) > vector.X) && (Position.X < (vector.X + 16f))) && (((Position.Y + Height) > vector.Y) && (Position.Y < (vector.Y + 16f))))
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        public static bool SolidTiles(int startX, int endX, int startY, int endY)
        {
            if (startX < 0)
            {
                return true;
            }
            if (endX >= Main.maxTilesX)
            {
                return true;
            }
            if (startY < 0)
            {
                return true;
            }
            if (endY >= Main.maxTilesY)
            {
                return true;
            }
            for (int i = startX; i < (endX + 1); i++)
            {
                for (int j = startY; j < (endY + 1); j++)
                {
                    if (Main.tile[i, j] == null)
                    {
                        return false;
                    }
                    if ((Main.tile[i, j].active && Main.tileSolid[Main.tile[i, j].type]) && !Main.tileSolidTop[Main.tile[i, j].type])
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public static Vector2 StickyTiles(Vector2 Position, Vector2 Velocity, int Width, int Height)
        {
            Vector2 vector2 = Position;
            int num = ((int) (Position.X / 16f)) - 1;
            int maxTilesX = ((int) ((Position.X + Width) / 16f)) + 2;
            int num3 = ((int) (Position.Y / 16f)) - 1;
            int maxTilesY = ((int) ((Position.Y + Height) / 16f)) + 2;
            if (num < 0)
            {
                num = 0;
            }
            if (maxTilesX > Main.maxTilesX)
            {
                maxTilesX = Main.maxTilesX;
            }
            if (num3 < 0)
            {
                num3 = 0;
            }
            if (maxTilesY > Main.maxTilesY)
            {
                maxTilesY = Main.maxTilesY;
            }
            for (int i = num; i < maxTilesX; i++)
            {
                for (int j = num3; j < maxTilesY; j++)
                {
                    if (((Main.tile[i, j] != null) && Main.tile[i, j].active) && (Main.tile[i, j].type == 0x33))
                    {
                        Vector2 vector;
                        vector.X = i * 0x10;
                        vector.Y = j * 0x10;
                        if ((((vector2.X + Width) > vector.X) && (vector2.X < (vector.X + 16f))) && (((vector2.Y + Height) > vector.Y) && (vector2.Y < (vector.Y + 16.01))))
                        {
                            if (((Math.Abs(Velocity.X) + Math.Abs(Velocity.Y)) > 0.7) && (Main.rand.Next(30) == 0))
                            {
                                Dust.NewDust(new Vector2((float) (i * 0x10), (float) (j * 0x10)), 0x10, 0x10, 30, 0f, 0f, 0, new Color(), 1f);
                            }
                            return new Vector2((float) i, (float) j);
                        }
                    }
                }
            }
            return new Vector2(-1f, -1f);
        }

        public static bool SwitchTiles(Vector2 Position, int Width, int Height, Vector2 oldPosition)
        {
            int num = ((int) (Position.X / 16f)) - 1;
            int maxTilesX = ((int) ((Position.X + Width) / 16f)) + 2;
            int num3 = ((int) (Position.Y / 16f)) - 1;
            int maxTilesY = ((int) ((Position.Y + Height) / 16f)) + 2;
            if (num < 0)
            {
                num = 0;
            }
            if (maxTilesX > Main.maxTilesX)
            {
                maxTilesX = Main.maxTilesX;
            }
            if (num3 < 0)
            {
                num3 = 0;
            }
            if (maxTilesY > Main.maxTilesY)
            {
                maxTilesY = Main.maxTilesY;
            }
            for (int i = num; i < maxTilesX; i++)
            {
                for (int j = num3; j < maxTilesY; j++)
                {
                    if (((Main.tile[i, j] != null) && Main.tile[i, j].active) && (Main.tile[i, j].type == 0x87))
                    {
                        Vector2 vector;
                        vector.X = i * 0x10;
                        vector.Y = (j * 0x10) + 12;
                        if (((((Position.X + Width) > vector.X) && (Position.X < (vector.X + 16f))) && (((Position.Y + Height) > vector.Y) && (Position.Y < (vector.Y + 4.01)))) && ((((oldPosition.X + Width) <= vector.X) || (oldPosition.X >= (vector.X + 16f))) || (((oldPosition.Y + Height) <= vector.Y) || (oldPosition.Y >= (vector.Y + 16.01)))))
                        {
                            WorldGen.hitSwitch(i, j);
                            NetMessage.SendData(0x3b, -1, -1, "", i, (float) j, 0f, 0f, 0);
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        public static Vector2 TileCollision(Vector2 Position, Vector2 Velocity, int Width, int Height, bool fallThrough = false, bool fall2 = false)
        {
            up = false;
            down = false;
            Vector2 vector = Velocity;
            Vector2 vector2 = Velocity;
            Vector2 vector4 = Position + Velocity;
            Vector2 vector5 = Position;
            int num = ((int) (Position.X / 16f)) - 1;
            int maxTilesX = ((int) ((Position.X + Width) / 16f)) + 2;
            int num3 = ((int) (Position.Y / 16f)) - 1;
            int maxTilesY = ((int) ((Position.Y + Height) / 16f)) + 2;
            int num5 = -1;
            int num6 = -1;
            int num7 = -1;
            int num8 = -1;
            if (num < 0)
            {
                num = 0;
            }
            if (maxTilesX > Main.maxTilesX)
            {
                maxTilesX = Main.maxTilesX;
            }
            if (num3 < 0)
            {
                num3 = 0;
            }
            if (maxTilesY > Main.maxTilesY)
            {
                maxTilesY = Main.maxTilesY;
            }
            for (int i = num; i < maxTilesX; i++)
            {
                for (int j = num3; j < maxTilesY; j++)
                {
                    if (((Main.tile[i, j] != null) && Main.tile[i, j].active) && (Main.tileSolid[Main.tile[i, j].type] || (Main.tileSolidTop[Main.tile[i, j].type] && (Main.tile[i, j].frameY == 0))))
                    {
                        Vector2 vector3;
                        vector3.X = i * 0x10;
                        vector3.Y = j * 0x10;
                        if ((((vector4.X + Width) > vector3.X) && (vector4.X < (vector3.X + 16f))) && (((vector4.Y + Height) > vector3.Y) && (vector4.Y < (vector3.Y + 16f))))
                        {
                            if ((vector5.Y + Height) <= vector3.Y)
                            {
                                down = true;
                                if ((!Main.tileSolidTop[Main.tile[i, j].type] || !fallThrough) || ((Velocity.Y > 1f) && !fall2))
                                {
                                    num7 = i;
                                    num8 = j;
                                    if (num7 != num5)
                                    {
                                        vector.Y = vector3.Y - (vector5.Y + Height);
                                    }
                                }
                            }
                            else if (((vector5.X + Width) <= vector3.X) && !Main.tileSolidTop[Main.tile[i, j].type])
                            {
                                num5 = i;
                                num6 = j;
                                if (num6 != num8)
                                {
                                    vector.X = vector3.X - (vector5.X + Width);
                                }
                                if (num7 == num5)
                                {
                                    vector.Y = vector2.Y;
                                }
                            }
                            else if ((vector5.X >= (vector3.X + 16f)) && !Main.tileSolidTop[Main.tile[i, j].type])
                            {
                                num5 = i;
                                num6 = j;
                                if (num6 != num8)
                                {
                                    vector.X = (vector3.X + 16f) - vector5.X;
                                }
                                if (num7 == num5)
                                {
                                    vector.Y = vector2.Y;
                                }
                            }
                            else if ((vector5.Y >= (vector3.Y + 16f)) && !Main.tileSolidTop[Main.tile[i, j].type])
                            {
                                up = true;
                                num7 = i;
                                num8 = j;
                                vector.Y = (vector3.Y + 16f) - vector5.Y;
                                if (num8 == num6)
                                {
                                    vector.X = vector2.X;
                                }
                            }
                        }
                    }
                }
            }
            return vector;
        }

        public static Vector2 WaterCollision(Vector2 Position, Vector2 Velocity, int Width, int Height, bool fallThrough = false, bool fall2 = false)
        {
            Vector2 vector = Velocity;
            Vector2 vector3 = Position + Velocity;
            Vector2 vector4 = Position;
            int num = ((int) (Position.X / 16f)) - 1;
            int maxTilesX = ((int) ((Position.X + Width) / 16f)) + 2;
            int num3 = ((int) (Position.Y / 16f)) - 1;
            int maxTilesY = ((int) ((Position.Y + Height) / 16f)) + 2;
            if (num < 0)
            {
                num = 0;
            }
            if (maxTilesX > Main.maxTilesX)
            {
                maxTilesX = Main.maxTilesX;
            }
            if (num3 < 0)
            {
                num3 = 0;
            }
            if (maxTilesY > Main.maxTilesY)
            {
                maxTilesY = Main.maxTilesY;
            }
            for (int i = num; i < maxTilesX; i++)
            {
                for (int j = num3; j < maxTilesY; j++)
                {
                    if ((Main.tile[i, j] != null) && (Main.tile[i, j].liquid > 0))
                    {
                        Vector2 vector2;
                        int num7 = ((int) Math.Round((double) (((float) Main.tile[i, j].liquid) / 32f))) * 2;
                        vector2.X = i * 0x10;
                        vector2.Y = ((j * 0x10) + 0x10) - num7;
                        if (((((vector3.X + Width) > vector2.X) && (vector3.X < (vector2.X + 16f))) && (((vector3.Y + Height) > vector2.Y) && (vector3.Y < (vector2.Y + num7)))) && (((vector4.Y + Height) <= vector2.Y) && !fallThrough))
                        {
                            vector.Y = vector2.Y - (vector4.Y + Height);
                        }
                    }
                }
            }
            return vector;
        }

        public static bool WetCollision(Vector2 Position, int Width, int Height)
        {
            Vector2 vector2 = new Vector2(Position.X + (Width / 2), Position.Y + (Height / 2));
            int num = 10;
            int num2 = Height / 2;
            if (num > Width)
            {
                num = Width;
            }
            if (num2 > Height)
            {
                num2 = Height;
            }
            vector2 = new Vector2(vector2.X - (num / 2), vector2.Y - (num2 / 2));
            int num3 = ((int) (Position.X / 16f)) - 1;
            int maxTilesX = ((int) ((Position.X + Width) / 16f)) + 2;
            int num5 = ((int) (Position.Y / 16f)) - 1;
            int maxTilesY = ((int) ((Position.Y + Height) / 16f)) + 2;
            if (num3 < 0)
            {
                num3 = 0;
            }
            if (maxTilesX > Main.maxTilesX)
            {
                maxTilesX = Main.maxTilesX;
            }
            if (num5 < 0)
            {
                num5 = 0;
            }
            if (maxTilesY > Main.maxTilesY)
            {
                maxTilesY = Main.maxTilesY;
            }
            for (int i = num3; i < maxTilesX; i++)
            {
                for (int j = num5; j < maxTilesY; j++)
                {
                    if ((Main.tile[i, j] != null) && (Main.tile[i, j].liquid > 0))
                    {
                        Vector2 vector;
                        vector.X = i * 0x10;
                        vector.Y = j * 0x10;
                        int num9 = 0x10;
                        float num10 = 0x100 - Main.tile[i, j].liquid;
                        num10 /= 32f;
                        vector.Y += num10 * 2f;
                        num9 -= (int) (num10 * 2f);
                        if ((((vector2.X + num) > vector.X) && (vector2.X < (vector.X + 16f))) && (((vector2.Y + num2) > vector.Y) && (vector2.Y < (vector.Y + num9))))
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }
    }
}


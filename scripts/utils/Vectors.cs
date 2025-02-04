namespace RSLib.GE
{
    using Godot;

    public static class Vectors
    {
        #region CLAMP V2
        public static Vector2 ClampAll(this Vector2 v, float min, float max)
        {
            return new Vector2(Mathf.Clamp(v.X, min, max), Mathf.Clamp(v.Y, min, max));
        }

        public static Vector2I ClampAll(this Vector2I v, int min, int max)
        {
            return new Vector2I(Mathf.Clamp(v.X, min, max), Mathf.Clamp(v.Y, min, max));
        }

        public static Vector2 ClampAll(this Vector2 v, Vector2 range)
        {
            return new Vector2(Mathf.Clamp(v.X, range.X, range.Y), Mathf.Clamp(v.Y, range.X, range.Y));
        }

        public static Vector2I ClampAll(this Vector2I v, Vector2I range)
        {
            return new Vector2I(Mathf.Clamp(v.X, range.X, range.Y), Mathf.Clamp(v.Y, range.X, range.Y));
        }

        public static Vector2 ClampX(this Vector2 v, float min, float max)
        {
            return new Vector2(Mathf.Clamp(v.X, min, max), v.Y);
        }

        public static Vector2I ClampX(this Vector2I v, int min, int max)
        {
            return new Vector2I(Mathf.Clamp(v.X, min, max), v.Y);
        }

        public static Vector2 ClampX(this Vector2 v, Vector2 range)
        {
            return new Vector2(Mathf.Clamp(v.X, range.X, range.Y), v.Y);
        }

        public static Vector2I ClampX(this Vector2I v, Vector2I range)
        {
            return new Vector2I(Mathf.Clamp(v.X, range.X, range.Y), v.Y);
        }

        public static Vector2 ClampY(this Vector2 v, float min, float max)
        {
            return new Vector2(v.X, Mathf.Clamp(v.Y, min, max));
        }

        public static Vector2I ClampY(this Vector2I v, int min, int max)
        {
            return new Vector2I(v.X, Mathf.Clamp(v.Y, min, max));
        }

        public static Vector2 ClampY(this Vector2 v, Vector2 range)
        {
            return new Vector2(v.X, Mathf.Clamp(v.Y, range.X, range.Y));
        }

        public static Vector2I ClampY(this Vector2I v, Vector2I range)
        {
            return new Vector2I(v.X, Mathf.Clamp(v.Y, range.X, range.Y));
        }

        public static Vector2 ClampX01(this Vector2 v)
        {
            return new Vector2(Mathf.Clamp(v.X, 0f, 1f), v.Y);
        }

        public static Vector2I ClampX01(this Vector2I v)
        {
            return new Vector2I(Mathf.Clamp(v.X, 0, 1), v.Y);
        }

        public static Vector2 ClampY01(this Vector2 v)
        {
            return new Vector2(v.X, Mathf.Clamp(v.Y, 0f, 1f));
        }

        public static Vector2I ClampY01(this Vector2I v)
        {
            return new Vector2I(v.X, Mathf.Clamp(v.Y, 0, 1));
        }
        #endregion // CLAMP V2

        #region CLAMP V3
        // TODO: Clamp V3
        #endregion // CLAMP V3

        #region CLAMP V4
        // TODO: Clamp V4
        #endregion

        #region LERP
        // TODO
        // static func lerp_xy(vector: Vector2, weight: float) -> float:
        // 	return lerp(vector.x, vector.y, weight)

        // TODO
        // static func lerp_xy_clamped(vector: Vector2, weight: float) -> float:
        // 	return clampf(lerp(vector.x, vector.y, weight), 0.0, 1.0)
        #endregion // LERP

        #region MIN/MAX
        // TODO
        // static func min_axis(vector: Variant) -> float:
        // 	return vector[vector.min_axis_index()]

        // TODO
        // static func max_axis(vector: Variant) -> float:
        // 	return vector[vector.max_axis_index()]
        #endregion // MIN/MAX

        #region MODULO
        public static Vector2I Mod(this Vector2I v, int mod)
        {
            return new Vector2I(v.X % mod, v.Y % mod);
        }

        public static Vector3I Mod(this Vector3I v, int mod)
        {
            return new Vector3I(v.X % mod, v.Y % mod, v.Z % mod);
        }

        public static Vector4I Mod(this Vector4I v, int mod)
        {
            return new Vector4I(v.X % mod, v.Y % mod, v.Z % mod, v.W % mod);
        }

        public static Vector2I Mod(this Vector2I v, Vector2I mod)
        {
            return new Vector2I(v.X % mod.X, v.Y % mod.Y);
        }

        public static Vector3I Mod(this Vector3I v, Vector3I mod)
        {
            return new Vector3I(v.X % mod.X, v.Y % mod.Y, v.Z % mod.Z);
        }

        public static Vector4I Mod(this Vector4I v, Vector4I mod)
        {
            return new Vector4I(v.X % mod.X, v.Y % mod.Y, v.Z % mod.Z, v.W % mod.W);
        }

        public static Vector2I PosMod(this Vector2I v, int mod)
        {
            return new Vector2I(Mathf.PosMod(v.X, mod), Mathf.PosMod(v.Y, mod));
        }

        public static Vector3I PosMod(this Vector3I v, int mod)
        {
            return new Vector3I(Mathf.PosMod(v.X, mod), Mathf.PosMod(v.Y, mod), Mathf.PosMod(v.Z, mod));
        }

        public static Vector4I PosMod(this Vector4I v, int mod)
        {
            return new Vector4I(Mathf.PosMod(v.X, mod), Mathf.PosMod(v.Y, mod), Mathf.PosMod(v.Z, mod), Mathf.PosMod(v.W, mod));
        }

        public static Vector2I PosMod(this Vector2I v, Vector2I mod)
        {
            return new Vector2I(Mathf.PosMod(v.X, mod.X), Mathf.PosMod(v.Y, mod.Y));
        }

        public static Vector3I PosMod(this Vector3I v, Vector3I mod)
        {
            return new Vector3I(Mathf.PosMod(v.X, mod.X), Mathf.PosMod(v.Y, mod.Y), Mathf.PosMod(v.Z, mod.Z));
        }

        public static Vector4I PosMod(this Vector4I v, Vector4I mod)
        {
            return new Vector4I(Mathf.PosMod(v.X, mod.X), Mathf.PosMod(v.Y, mod.Y), Mathf.PosMod(v.Z, mod.Z), Mathf.PosMod(v.W, mod.W));
        }
        #endregion // MODULO

        #region RANDOM
        public static float Random(this Vector2 v)
        {
            return (float)GD.RandRange(v.X, v.Y);
        }

        public static int Random(this Vector2I v)
        {
            return GD.RandRange(v.X, v.Y);
        }

        public static Vector2 RandomVector2()
        {
            return new Vector2(GD.RandRange(-1, 1), GD.RandRange(-1, 1)).Normalized();
        }

        public static Vector3 RandomVector3()
        {
            return new Vector3(GD.RandRange(-1, 1), GD.RandRange(-1, 1), GD.RandRange(-1, 1)).Normalized();
        }

        public static Vector4 RandomVector4()
        {
            return new Vector4(GD.RandRange(-1, 1), GD.RandRange(-1, 1), GD.RandRange(-1, 1), GD.RandRange(-1, 1)).Normalized();
        }
        #endregion // RANDOM
    }
}
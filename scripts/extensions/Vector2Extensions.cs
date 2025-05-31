namespace RSLib.GE
{
    using Godot;

    public static class Vector2Extensions
    {
        #region ABS
        
        /// <summary>
        /// Gets a vector copy with all components absolute values.
        /// </summary>
        public static Vector2 AbsAll(this Vector2 v)
        {
            return new Vector2(Mathf.Abs(v.X), Mathf.Abs(v.Y));
        }

        /// <summary>
        /// Gets a vector copy with all components absolute values.
        /// </summary>
        public static Vector2I AbsAll(this Vector2I v)
        {
            return new Vector2I(Mathf.Abs(v.X), Mathf.Abs(v.Y));
        }

        /// <summary>
        /// Gets a vector copy with x component absolute value.
        /// </summary>
        public static Vector2 AbsX(this Vector2 v)
        {
            return new Vector2(Mathf.Abs(v.X), v.Y);
        }

        /// <summary>
        /// Gets a vector copy with x component absolute value.
        /// </summary>
        public static Vector2I AbsX(this Vector2I v)
        {
            return new Vector2I(Mathf.Abs(v.X), v.Y);
        }

        /// <summary>
        /// Gets a vector copy with y component absolute value.
        /// </summary>
        public static Vector2 AbsY(this Vector2 v)
        {
            return new Vector2(v.X, Mathf.Abs(v.Y));
        }

        /// <summary>
        /// Gets a vector copy with y component absolute value.
        /// </summary>
        public static Vector2I AbsY(this Vector2I v)
        {
            return new Vector2I(v.X, Mathf.Abs(v.Y));
        }
        
        #endregion // ABS

        #region ADD
        
        /// <summary>
        /// Gets a vector's copy with all components incremented.
        /// </summary>
        /// <param name="v">Vector to increment.</param>
        /// <param name="value">Incrementation amount.</param>
        public static Vector2 AddAll(this Vector2 v, float value)
        {
            return new Vector2(v.X + value, v.Y + value);
        }

        /// <summary>
        /// Gets a vector's copy with all components incremented.
        /// </summary>
        /// <param name="v">Vector to increment.</param>
        /// <param name="value">Incrementation amount.</param>
        public static Vector2I AddAll(this Vector2I v, int value)
        {
            return new Vector2I(v.X + value, v.Y + value);
        }

        /// <summary>
        /// Gets a vector's copy with x component incremented.
        /// </summary>
        /// <param name="v">Vector to increment.</param>
        /// <param name="value">Incrementation amount.</param>
        public static Vector2 AddX(this Vector2 v, float value)
        {
            return new Vector2(v.X + value, v.Y);
        }

        /// <summary>
        /// Gets a vector's copy with x component incremented.
        /// </summary>
        /// <param name="v">Vector to increment.</param>
        /// <param name="value">Incrementation amount.</param>
        public static Vector2I AddX(this Vector2I v, int value)
        {
            return new Vector2I(v.X + value, v.Y);
        }

        /// <summary>
        /// Gets a vector's copy with y component incremented.
        /// </summary>
        /// <param name="v">Vector to increment.</param>
        /// <param name="value">Incrementation amount.</param>
        public static Vector2 AddY(this Vector2 v, float value)
        {
            return new Vector2(v.X, v.Y + value);
        }

        /// <summary>
        /// Gets a vector's copy with y component incremented.
        /// </summary>
        /// <param name="v">Vector to increment.</param>
        /// <param name="value">Incrementation amount.</param>
        public static Vector2I AddY(this Vector2I v, int value)
        {
            return new Vector2I(v.X, v.Y + value);
        }
        
        #endregion // ADD

        #region CLAMP
        
        /// <summary>
        /// Gets a vector copy with all components clamped between two values.
        /// </summary>
        /// <param name="v">Vector to clamp.</param>
        /// <param name="min">Minimum value.</param>
        /// <param name="max">Maximum value.</param>
        public static Vector2 ClampAll(this Vector2 v, float min, float max)
        {
            return new Vector2(Mathf.Clamp(v.X, min, max), Mathf.Clamp(v.Y, min, max));
        }

        /// <summary>
        /// Gets a vector copy with all components clamped between two values.
        /// </summary>
        /// <param name="v">Vector to clamp.</param>
        /// <param name="min">Minimum value.</param>
        /// <param name="max">Maximum value.</param>
        public static Vector2I ClampAll(this Vector2I v, int min, int max)
        {
            return new Vector2I(Mathf.Clamp(v.X, min, max), Mathf.Clamp(v.Y, min, max));
        }

        /// <summary>
        /// Gets a vector copy with all components clamped between two values.
        /// </summary>
        /// <param name="v">Vector to clamp.</param>
        /// <param name="min">Minimum values.</param>
        /// <param name="max">Maximum values.</param>
        public static Vector2 ClampAll(this Vector2 v, Vector2 min, Vector2 max)
        {
            return new Vector2(Mathf.Clamp(v.X, min.X, max.X), Mathf.Clamp(v.Y, min.Y, max.Y));
        }

        /// <summary>
        /// Gets a vector copy with all components clamped between two values.
        /// </summary>
        /// <param name="v">Vector to clamp.</param>
        /// <param name="min">Minimum values.</param>
        /// <param name="max">Maximum values.</param>
        public static Vector2I ClampAll(this Vector2I v, Vector2I min, Vector2I max)
        {
            return new Vector2I(Mathf.Clamp(v.X, min.X, max.X), Mathf.Clamp(v.Y, min.Y, max.Y));
        }

        /// <summary>
        /// Gets a vector copy with x component clamped between two values.
        /// </summary>
        /// <param name="v">Vector to clamp.</param>
        /// <param name="min">Minimum value.</param>
        /// <param name="max">Maximum value.</param>
        public static Vector2 ClampX(this Vector2 v, float min, float max)
        {
            return new Vector2(Mathf.Clamp(v.X, min, max), v.Y);
        }

        /// <summary>
        /// Gets a vector copy with x component clamped between two values.
        /// </summary>
        /// <param name="v">Vector to clamp.</param>
        /// <param name="min">Minimum value.</param>
        /// <param name="max">Maximum value.</param>
        public static Vector2I ClampX(this Vector2I v, int min, int max)
        {
            return new Vector2I(Mathf.Clamp(v.X, min, max), v.Y);
        }

        /// <summary>
        /// Gets a vector copy with y component clamped between two values.
        /// </summary>
        /// <param name="v">Vector to clamp.</param>
        /// <param name="min">Minimum value.</param>
        /// <param name="max">Maximum value.</param>
        public static Vector2 ClampY(this Vector2 v, float min, float max)
        {
            return new Vector2(v.X, Mathf.Clamp(v.Y, min, max));
        }

        /// <summary>
        /// Gets a vector copy with y component clamped between two values.
        /// </summary>
        /// <param name="v">Vector to clamp.</param>
        /// <param name="min">Minimum value.</param>
        /// <param name="max">Maximum value.</param>
        public static Vector2I ClampY(this Vector2I v, int min, int max)
        {
            return new Vector2I(v.X, Mathf.Clamp(v.Y, min, max));
        }

        /// <summary>
        /// Gets a vector copy with all components clamped between 0 and 1.
        /// </summary>
        public static Vector2 ClampAll01(this Vector2 v)
        {
            return new Vector2(Mathf.Clamp(v.X, 0f, 1f), Mathf.Clamp(v.Y, 0f, 1f));
        }

        /// <summary>
        /// Gets a vector copy with x component clamped between 0 and 1.
        /// </summary>
        public static Vector2 ClampX01(this Vector2 v)
        {
            return new Vector2(Mathf.Clamp(v.X, 0f, 1f), v.Y);
        }

        /// <summary>
        /// Gets a vector copy with y component clamped between 0 and 1.
        /// </summary>
        public static Vector2 ClampY01(this Vector2 v)
        {
            return new Vector2(v.X, Mathf.Clamp(v.Y, 0, 1));
        }
        
        #endregion // CLAMP
        
        #region CONVERSION

        /// <summary>
        /// Converts a Vector2 to a Vector2I.
        /// </summary>
        /// <returns>New Vector2I.</returns>
        public static Vector2I ToVector2Int(this Vector2 v)
        {
            return new Vector2I((int)v.X, (int)v.Y);
        }

        /// <summary>
        /// Converts a Vector2I to a Vector2.
        /// </summary>
        /// <returns>New Vector2.</returns>
        public static Vector2 ToVector2(this Vector2I v)
        {
            return new Vector2(v.X, v.Y);
        }
        
        /// <summary>
        /// Converts a Vector2 to a Vector3.
        /// </summary>
        /// <returns>New Vector3.</returns>
        public static Vector3 ToVector3(this Vector2 v, float z = 0f)
        {
            return new Vector3(v.X, v.Y, z);
        }
        
        /// <summary>
        /// Converts a Vector2I to a Vector3I.
        /// </summary>
        /// <returns>New Vector3I.</returns>
        public static Vector3I ToVector3(this Vector2I v, int z = 0)
        {
            return new Vector3I(v.X, v.Y, z);
        }

        /// <summary>
        /// Converts a Vector2 collection to a Vector3 array.
        /// </summary>
        /// <returns>New Vector3 array.</returns>
        public static Vector3[] ToVector3Array(this System.Collections.Generic.IEnumerable<Vector2> vectors)
        {
            System.Collections.Generic.List<Vector3> list = new System.Collections.Generic.List<Vector3>();
            foreach (Vector2 v in vectors)
                list.Add(v.ToVector3());

            return list.ToArray();
        }

        /// <summary>
        /// Converts a Vector2I collection to a Vector3I array.
        /// </summary>
        /// <returns>New Vector3I array.</returns>
        public static Vector3I[] ToVector3IArray(this System.Collections.Generic.IEnumerable<Vector2> vectors)
        {
            System.Collections.Generic.List<Vector3I> list = new System.Collections.Generic.List<Vector3I>();
            foreach (Vector2I v in vectors)
                list.Add(v.ToVector3());

            return list.ToArray();
        }

        /// <summary>
        /// Converts a Vector2 to a direction.
        /// </summary>
        /// <returns>Direction.</returns>
        public static Direction ToDirection(this Vector2 v)
        {
            return Mathf.Abs(v.X) > Mathf.Abs(v.Y) ? v.X < 0f ? Direction.LEFT : Direction.RIGHT : v.Y < 0f ? Direction.UP : Direction.DOWN;
        }
        
        #endregion // CONVERSION
        
        #region LERP

        /// <summary>
        /// Computes the linear interpolation between x and y components using an interpolation value.
        /// </summary>
        /// <param name="v">Vector to use as lerp start and end values.</param>
        /// <param name="t">Interpolation value.</param>
        /// <returns>Interpolated value.</returns>
        public static float Lerp(this Vector2 v, float t)
        {
            return Mathf.Lerp(v.X, v.Y, t);
        }

        /// <summary>
        /// Computes the unclamped linear interpolation between x and y components using an interpolation value.
        /// </summary>
        /// <param name="v">Vector to use as lerp start and end values.</param>
        /// <param name="t">Interpolation value.</param>
        /// <returns>Interpolated value.</returns>
        public static float LerpUnclamped(this Vector2 v, float t)
        {
            return v.X * (1f - t) + v.Y * t;
        }

        #endregion // LERP

        #region MIN/MAX

        /// <summary>
        /// Returns the smallest of the vector's components.
        /// </summary>
        /// <returns>Smallest vector component.</returns>
        public static float Min(this Vector2 v)
        {
            return Mathf.Min(v.X, v.Y);
        }
        
        /// <summary>
        /// Returns the smallest of the vector's components.
        /// </summary>
        /// <returns>Smallest vector component.</returns>
        public static int Min(this Vector2I v)
        {
            return Mathf.Min(v.X, v.Y);
        }
        
        /// <summary>
        /// Returns the largest of the vector's components.
        /// </summary>
        /// <returns>Largest vector component.</returns>
        public static float Max(this Vector2 v)
        {
            return Mathf.Max(v.X, v.Y);
        }
        
        /// <summary>
        /// Returns the largest of the vector's components.
        /// </summary>
        /// <returns>Largest vector component.</returns>
        public static int Max(this Vector2I v)
        {
            return Mathf.Max(v.X, v.Y);
        }

        #endregion // MIN/MAX

        #region MODULO
        
        /// <summary>
        /// Gets a vector copy with a modulo applied on all components.
        /// </summary>
        /// <param name="v">Vector to apply modulo to.</param>
        /// <param name="mod">Modulo value.</param>
        public static Vector2I Mod(this Vector2I v, int mod)
        {
            return new Vector2I(v.X % mod, v.Y % mod);
        }

        /// <summary>
        /// Gets a vector copy with a modulo applied on all components.
        /// </summary>
        /// <param name="v">Vector to apply modulo to.</param>
        /// <param name="mod">Modulo value.</param>
        public static Vector3I Mod(this Vector3I v, int mod)
        {
            return new Vector3I(v.X % mod, v.Y % mod, v.Z % mod);
        }

        /// <summary>
        /// Gets a vector copy with a modulo applied on all components.
        /// </summary>
        /// <param name="v">Vector to apply modulo to.</param>
        /// <param name="mod">Modulo value.</param>
        public static Vector4I Mod(this Vector4I v, int mod)
        {
            return new Vector4I(v.X % mod, v.Y % mod, v.Z % mod, v.W % mod);
        }
        
        /// <summary>
        /// Gets a vector copy with a modulo applied on all components.
        /// </summary>
        /// <param name="v">Vector to apply modulo to.</param>
        /// <param name="mod">Modulo values.</param>
        public static Vector2I Mod(this Vector2I v, Vector2I mod)
        {
            return new Vector2I(v.X % mod.X, v.Y % mod.Y);
        }

        /// <summary>
        /// Gets a vector copy with a modulo applied on all components.
        /// </summary>
        /// <param name="v">Vector to apply modulo to.</param>
        /// <param name="mod">Modulo values.</param>
        public static Vector3I Mod(this Vector3I v, Vector3I mod)
        {
            return new Vector3I(v.X % mod.X, v.Y % mod.Y, v.Z % mod.Z);
        }

        /// <summary>
        /// Gets a vector copy with a modulo applied on all components.
        /// </summary>
        /// <param name="v">Vector to apply modulo to.</param>
        /// <param name="mod">Modulo values.</param>
        public static Vector4I Mod(this Vector4I v, Vector4I mod)
        {
            return new Vector4I(v.X % mod.X, v.Y % mod.Y, v.Z % mod.Z, v.W % mod.W);
        }

        /// <summary>
        /// Gets a vector copy with a canonical modulo applied on all components.
        /// </summary>
        /// <param name="v">Vector to apply modulo to.</param>
        /// <param name="mod">Modulo values.</param>
        public static Vector2I PosMod(this Vector2I v, int mod)
        {
            return new Vector2I(Mathf.PosMod(v.X, mod), Mathf.PosMod(v.Y, mod));
        }

        /// <summary>
        /// Gets a vector copy with a canonical modulo applied on all components.
        /// </summary>
        /// <param name="v">Vector to apply modulo to.</param>
        /// <param name="mod">Modulo values.</param>
        public static Vector3I PosMod(this Vector3I v, int mod)
        {
            return new Vector3I(Mathf.PosMod(v.X, mod), Mathf.PosMod(v.Y, mod), Mathf.PosMod(v.Z, mod));
        }

        /// <summary>
        /// Gets a vector copy with a canonical modulo applied on all components.
        /// </summary>
        /// <param name="v">Vector to apply modulo to.</param>
        /// <param name="mod">Modulo values.</param>
        public static Vector4I PosMod(this Vector4I v, int mod)
        {
            return new Vector4I(Mathf.PosMod(v.X, mod), Mathf.PosMod(v.Y, mod), Mathf.PosMod(v.Z, mod), Mathf.PosMod(v.W, mod));
        }

        /// <summary>
        /// Gets a vector copy with a canonical modulo applied on all components.
        /// </summary>
        /// <param name="v">Vector to apply modulo to.</param>
        /// <param name="mod">Modulo values.</param>
        public static Vector2I PosMod(this Vector2I v, Vector2I mod)
        {
            return new Vector2I(Mathf.PosMod(v.X, mod.X), Mathf.PosMod(v.Y, mod.Y));
        }

        /// <summary>
        /// Gets a vector copy with a canonical modulo applied on all components.
        /// </summary>
        /// <param name="v">Vector to apply modulo to.</param>
        /// <param name="mod">Modulo values.</param>
        public static Vector3I PosMod(this Vector3I v, Vector3I mod)
        {
            return new Vector3I(Mathf.PosMod(v.X, mod.X), Mathf.PosMod(v.Y, mod.Y), Mathf.PosMod(v.Z, mod.Z));
        }

        /// <summary>
        /// Gets a vector copy with a canonical modulo applied on all components.
        /// </summary>
        /// <param name="v">Vector to apply modulo to.</param>
        /// <param name="mod">Modulo values.</param>
        public static Vector4I PosMod(this Vector4I v, Vector4I mod)
        {
            return new Vector4I(Mathf.PosMod(v.X, mod.X), Mathf.PosMod(v.Y, mod.Y), Mathf.PosMod(v.Z, mod.Z), Mathf.PosMod(v.W, mod.W));
        }
        
        #endregion // MODULO
        
        #region NORMAL

        /// <summary>
        /// Computes the vector normal. Use NormalNormalized to get it normalized.
        /// </summary>
        /// <param name="v">Vector to get the normal of.</param>
        /// <param name="clockwise">Normal is rotated clockwise.</param>
        /// <returns>Normal as a new vector.</returns>
        public static Vector2 Normal(this Vector2 v, bool clockwise = true)
        {
            Vector2 normal = v;

            normal.X += normal.Y;
            normal.Y = normal.X - normal.Y;
            normal.X -= normal.Y;
            normal.Y *= -1;

            return clockwise ? normal : -normal;
        }

        /// <summary>
        /// Computes the vector normalized normal.
        /// </summary>
        /// <param name="v">Vector to get the normalized normal of.</param>
        /// <param name="clockwise">Normal is rotated clockwise.</param>
        /// <returns>Normalized normal as a new vector.</returns>
        public static Vector2 NormalNormalized(this Vector2 v, bool clockwise = true)
        {
            return v.Normal(clockwise).Normalized();
        }

        #endregion // NORMAL
        
        #region RANDOM
        
        /// <summary>
        /// Gets a random float between vector's x component and y component values.
        /// </summary>
        /// <returns>Randomly generated number.</returns>
        public static float Random(this Vector2 v)
        {
            return (float)GD.RandRange(v.X, v.Y);
        }

        /// <summary>
        /// Gets a random float between vector's x component and y component values.
        /// </summary>
        /// <returns>Randomly generated number.</returns>
        public static int Random(this Vector2I v)
        {
            return GD.RandRange(v.X, v.Y);
        }

        #endregion // RANDOM
        
        #region ROUND

        /// <summary>
        /// Gets a vector's copy with all components rounded.
        /// </summary>
        public static Vector2 RoundAll(this Vector2 v)
        {
            return new Vector2(Mathf.Round(v.X), Mathf.Round(v.Y));
        }

        /// <summary>
        /// Gets a vector's copy with x component rounded.
        /// </summary>
        public static Vector2 RoundX(this Vector2 v)
        {
            return new Vector2(Mathf.Round(v.X), v.Y);
        }

        /// <summary>
        /// Gets a vector's copy with y component rounded.
        /// </summary>
        public static Vector2 RoundY(this Vector2 v)
        {
            return new Vector2(v.X, Mathf.Round(v.Y));
        }

        #endregion // ROUND

        #region SWAP

        /// <summary>
        /// Swaps x and y components of a vector.
        /// </summary>
        public static Vector2 Swap(this Vector2 v)
        {
            return new Vector2(v.Y, v.X);
        }

        #endregion // SWAP

        #region WITH

        /// <summary>
        /// Gets a vector's copy with new x value.
        /// </summary>
        /// <param name="v">Vector to modify.</param>
        /// <param name="value">New x value.</param>
        public static Vector2 WithX(this Vector2 v, float value)
        {
            return new Vector2(value, v.Y);
        }

        /// <summary>
        /// Gets a vector's copy with new x value.
        /// </summary>
        /// <param name="v">Vector to modify.</param>
        /// <param name="value">New x value.</param>
        public static Vector2 WithX(this Vector2 v, int value)
        {
            return new Vector2(value, v.Y);
        }

        /// <summary>
        /// Gets a vector's copy with new y value.
        /// </summary>
        /// <param name="v">Vector to modify.</param>
        /// <param name="value">New y value.</param>
        public static Vector2 WithY(this Vector2 v, float value)
        {
            return new Vector2(v.X, value);
        }

        /// <summary>
        /// Gets a vector's copy with new y value.
        /// </summary>
        /// <param name="v">Vector to modify.</param>
        /// <param name="value">New y value.</param>
        public static Vector2 WithY(this Vector2 v, int value)
        {
            return new Vector2(v.X, value);
        }

        #endregion // WITH
    }
}
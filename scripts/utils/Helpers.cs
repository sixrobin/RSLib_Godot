namespace RSLib.GE
{
    using Godot;

    public static class Helpers
    {
        #region RANDOM
        
        /// <summary>
        /// Gets a random boolean (true or false).
        /// </summary>
        /// <returns>Randomly generated boolean.</returns>
        public static bool RandomBool()
        {
            return GD.Randf() < 0.5f;
        }

        /// <summary>
        /// Gets a new random normalized Vector2.
        /// </summary>
        /// <returns>Randomly generated vector.</returns>
        public static Vector2 RandomVector2()
        {
            return new Vector2((float)GD.RandRange(-1f, 1f), (float)GD.RandRange(-1f, 1f)).Normalized();
        }

        /// <summary>
        /// Gets a new random normalized Vector3.
        /// </summary>
        /// <returns>Randomly generated vector.</returns>
        public static Vector3 RandomVector3()
        {
            return new Vector3((float)GD.RandRange(-1f, 1f), (float)GD.RandRange(-1f, 1f), (float)GD.RandRange(-1f, 1f)).Normalized();
        }

        /// <summary>
        /// Gets a new random normalized Vector4.
        /// </summary>
        /// <returns>Randomly generated vector.</returns>
        public static Vector4 RandomVector4()
        {
            return new Vector4((float)GD.RandRange(-1f, 1f), (float)GD.RandRange(-1f, 1f), (float)GD.RandRange(-1f, 1f), (float)GD.RandRange(-1f, 1f)).Normalized();
        }
        
        /// <summary>
        /// Gets a new random color.
        /// <param name="randomAlpha">Randomize alpha.</param>
        /// </summary>
        /// <returns>Randomly generated color.</returns>
        public static Color RandomColor(bool randomAlpha = false)
        {
            return new Color(GD.Randf(), GD.Randf(), GD.Randf(), randomAlpha ? GD.Randf() : 1f);
        }
        
        #endregion // RANDOM
        
        public static string FormatByteSize(ulong bytes, bool round = false)
        {
            string[] suffixes = {"bytes", "KB", "MB", "GB", "TB", "PB"};
            int counter = 0;
            float number = bytes;

            while (Mathf.Round(number / 1024f) >= 1)
            {
                number /= 1024f;
                counter++;
            }

            return (round ? Mathf.Round(number) : number) + suffixes[counter];
        }
    }
}
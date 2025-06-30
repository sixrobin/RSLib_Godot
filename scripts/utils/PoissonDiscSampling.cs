using Godot;
using System;
using System.Collections.Generic;

public static class PoissonDiscDistribution
{
    /// <summary>Generate points (Vector2) using the Poisson-Disc algorithm.</summary>
    /// <param name="pointsCount">The number of points to generate.</param>
    /// <param name="candidatePointsCount">The number of candidate points the algorithm will generate between each iteration.</param>
    /// <param name="gridSize">The size of the grid. Points will be generated between (0,0) and the grid size.</param>
    /// <param name="existingPoints">Points that were here prior to the generation. They will be avoided as well as any other point, and won't be added in the output.</param>
    /// <returns>The generated points.</returns>
    public static HashSet<Vector2> GeneratePoints(int pointsCount, int candidatePointsCount, Vector2 gridSize, HashSet<Vector2> existingPoints)
    {
        if (gridSize == Vector2.Zero)
            throw new ArgumentException($"{nameof(PoissonDiscDistribution)} argument {nameof(gridSize)} cannot be zero");
        
        return GeneratePoints(pointsCount,
                              candidatePointsCount, 
                              () => new Vector2(GD.Randf() * gridSize.X, GD.Randf() * gridSize.Y),
                              (a, b) => a.DistanceTo(b),
                              existingPoints);
    }

    /// <summary>Generate points (generic) using the Poisson-Disc algorithm.</summary>
    /// <param name="pointsCount">The number of points to generate.</param>
    /// <param name="candidatePointsCount">The number of candidate points the algorithm will generate between each iteration.</param>
    /// <param name="randomPointGetter">How to get a random point with the specified point type.</param>
    /// <param name="distanceGetter">How to get the distance between two points with the specified point type.</param>
    /// <param name="existingPoints">Points that were here previous to the generation. They will be avoided as well as any other point, and won't be added in the output.</param>
    /// <returns>The generated points.</returns>
    public static HashSet<TPoint> GeneratePoints<TPoint>(int pointsCount, int candidatePointsCount, Func<TPoint> randomPointGetter, Func<TPoint, TPoint, float> distanceGetter, HashSet<TPoint> existingPoints)
    {
        HashSet<TPoint> points = new(pointsCount);
        for (int i = 0; i < pointsCount; i++)
        {
            TPoint point = GeneratePoint(candidatePointsCount, randomPointGetter, distanceGetter, existingPoints);
            points.Add(point);
            existingPoints.Add(point);
        }
        
        return points;
    }
    
    /// <summary>Generate one point (generic) using the Poisson-Disc algorithm.</summary>
    /// <param name="candidatePointsCount">The number of candidate points the algorithm will generate.</param>
    /// <param name="randomPointGetter">How to get a random point with the specified point type.</param>
    /// <param name="distanceGetter">How to get the distance between two points with the specified point type.</param>
    /// <param name="existingPoints">Points that were here previous to the generation. They will be avoided as well as any other point, and won't be added in the output.</param>
    /// <returns>The generated point.</returns>
    private static TPoint GeneratePoint<TPoint>(int candidatePointsCount, Func<TPoint> randomPointGetter, Func<TPoint, TPoint, float> distanceGetter, HashSet<TPoint> existingPoints)
    {
        // Create candidates points.
        Tuple<TPoint, float>[] candidatePoints = new Tuple<TPoint, float>[candidatePointsCount];
        for (int i = 0; i < candidatePointsCount; i++)
        {
            TPoint randomPosition = randomPointGetter();
            float closestDistance = float.MaxValue;
            
            // Fetch the closest distance amongst existing points (prior to the generation).
            foreach (TPoint existingPoint in existingPoints)
            {
                float distance = distanceGetter(randomPosition, existingPoint);
                if (distance < closestDistance)
                    closestDistance = distance;
            }

            candidatePoints[i] = new Tuple<TPoint, float>(randomPosition, closestDistance);
        }
        
        // Find the best candidate (biggest distance from current points).
        float farthestDistance = float.MinValue;
        int bestCandidateIndex = -1;

        for (int i = 0; i < candidatePointsCount; i++)
        {
            if (candidatePoints[i].Item2 > farthestDistance)
            {
                farthestDistance = candidatePoints[i].Item2;
                bestCandidateIndex = i;
            }
        }
        
        return candidatePoints[bestCandidateIndex].Item1;
    }
}

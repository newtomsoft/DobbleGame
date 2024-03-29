﻿namespace DobbleManager;

public class DobbleCard
{
    public List<int> PicturesIds { get; private set; } = new();

    public DobbleCard(int firstValue) => PicturesIds.Add(firstValue);

    public DobbleCard() { } // don't remove this constructor used by binding in controller !

    public void ShufflePictures() => PicturesIds = PicturesIds.OrderBy(_ => Guid.NewGuid()).ToList();
    public static bool operator ==(DobbleCard dc1, DobbleCard dc2) => dc1.PicturesIds.SequenceEqual(dc2.PicturesIds);
    public static bool operator !=(DobbleCard dc1, DobbleCard dc2) => !(dc1 == dc2);
    public override bool Equals(object obj) => PicturesIds.Equals(obj);
    public override int GetHashCode() => HashCode.Combine(PicturesIds);

    public override string ToString()
    {
        StringBuilder stringBuilder = new();
        foreach (var pictureId in PicturesIds)
            stringBuilder.Append(pictureId + "-");

        return stringBuilder.Remove(stringBuilder.Length - 1, 1).ToString();
    }


}
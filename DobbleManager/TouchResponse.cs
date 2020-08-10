namespace DobbleManager
{
    public struct TouchResponse
    {
        public TouchStatus Status { get; private set; }
        public DobbleCard CenterCard { get; private set; }

        public TouchResponse(TouchStatus status, DobbleCard centerCard = null)
        {
            Status = status;
            CenterCard = centerCard;
        }
    }
}

namespace Foundation
{
    public interface ITimeScaleManager
    {
        TimeScaleHandle BeginTimeScale(float scale);
        void EndTimeScale(TimeScaleHandle handle);
    }
}

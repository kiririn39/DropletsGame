namespace Project.Source.Progress
{
    public interface IProgressCountConsumer
    {
        void Consume(float progress);
    }
}
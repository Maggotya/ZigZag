
namespace Assets._Game.Scripts.Game.Objects.Platform
{
    interface IPlatformSizer
    {
        float size { get; set; }
        float thickness { get; set; }

        void SetSize(float size);
        void SetThickness(float thickness);
        void Set(float size, float thickness);
    }
}

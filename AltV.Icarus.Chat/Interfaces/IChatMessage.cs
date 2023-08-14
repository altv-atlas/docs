using AltV.Net;
using AltV.Net.Elements.Entities;
namespace AltV.Icarus.Chat.Interfaces;

public interface IChatMessage : IWritable
{
    public string Tag { get; set; }
    public string TagColor { get; set; }
    public IPlayer? Sender { get; set; }
    public string Text { get; set; }
    public string TextColor { get; set; }
}
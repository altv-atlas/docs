using AltV.Net.Elements.Entities;
namespace AltV.Icarus.Chat.Interfaces.Base;

public interface IChatBase
{
    public uint Id { get; set; }
    public string DefaultColor { get; set; }
    public string Prefix { get; set; }
    public string Tag { get; set; }
    public string TagColor { get; set; }

    public delegate void ChatMessageDelegate( IPlayer player, string message );

    public event ChatMessageDelegate OnChatMessage;

    public void SendMessage( string message );
    public void SendMessage( IPlayer sender, string message );
    public void SendMessage( IChatMessage message );
}
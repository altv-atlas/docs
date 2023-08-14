using AltV.Icarus.Chat.Interfaces.Base;
namespace AltV.Icarus.Chat.Interfaces;

public interface IAdvancedChat : IChatBase
{
    public uint Type { get; set; }
}
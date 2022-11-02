using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BloogBot.Game;
using BloogBot.Game.Enums;

namespace BloogBot.Game
{
    public unsafe abstract class Chat
    {
        public readonly int ChatCurrentIndex;
        public readonly CGGuid SenderGuid;
        public readonly string Message;
        public readonly int ChatTypeId;

        
        internal Chat(int chatCurrentIndex, CGGuid senderGuid, string message, int chatTypeId)
        {
            ChatCurrentIndex = chatCurrentIndex;
            SenderGuid = senderGuid;
            Message = message;
            ChatTypeId = chatTypeId;
        }
    }
    public class WhipserChat : Chat
    {
        internal WhipserChat(int chatCurrentIndex, CGGuid senderGuid, string message, int chatTypeId)
            : base(chatCurrentIndex, senderGuid, message, chatTypeId)
        {
        }
    }
    public class CommonChat : Chat
    {
        internal CommonChat(int chatCurrentIndex, CGGuid senderGuid, string message, int chatTypeId)
            : base(chatCurrentIndex, senderGuid, message, chatTypeId)
        {
        }
    }
    public class ChatManager
    {
        internal static IList<Chat> Chats = new List<Chat>();
        static internal IList<Chat> ChatsBuffer = new List<Chat>();


        static public IEnumerable<WhipserChat> LastChat => Chats.OfType<WhipserChat>().Where(x => (ChatTypeId)x.ChatTypeId == ChatTypeId.WHISPER);

        static bool IsChatInitialized => MemoryManager.ReadByte(IntPtr.Add(MemoryAddresses.MemBase, Offsets.ChatHistoryInitialized))==1;

        internal static void TraverseMessage()
        {
            if(IsChatInitialized)
            {
                int chatCurrentIndex;
                CGGuid sendGuid;
                string message;
                int chatTypeId;

                chatCurrentIndex = MemoryManager.ReadInt(IntPtr.Add(MemoryAddresses.MemBase, Offsets.ChatHistoryCurrentIndex));
                for (int i = 0; i < chatCurrentIndex + 1; i++)
                {
                    sendGuid = MemoryManager.ReadGuid(IntPtr.Add(MemoryAddresses.MemBase, Offsets.ChatHistory + i * Offsets.ChatHistoryNextOffset));
                    message = MemoryManager.ReadStringName(IntPtr.Add(MemoryAddresses.MemBase, Offsets.ChatHistory + i * Offsets.ChatHistoryNextOffset + Offsets.ChatHistoryFullMessage), Encoding.UTF8);
                    chatTypeId = MemoryManager.ReadInt(IntPtr.Add(MemoryAddresses.MemBase, Offsets.ChatHistory + i * Offsets.ChatHistoryNextOffset + Offsets.ChatHistoryChatType));
                    Console.WriteLine(i);
                    Console.WriteLine(sendGuid.high);
                    Console.WriteLine(chatTypeId);
                    Console.WriteLine(message);
                    try
                    {

                        if((ChatTypeId)chatTypeId == ChatTypeId.WHISPER)
                        {
                            ChatsBuffer.Add(new WhipserChat(i, sendGuid, message, chatTypeId));
                            /*
                            if (i == chatCurrentIndex)
                            {
                                var chat = new WhipserChat(i, sendGuid, message, chatTypeId);
                                LastChat = chat;
                                ChatsBuffer.Add(chat);
                            }
                            */
                        }
                        else
                        {
                            ChatsBuffer.Add(new CommonChat(i, sendGuid, message, chatTypeId));
                        }
                    }
                    catch (Exception e)
                    {
                        //Logger.Log(e);
                    }
                }
                Chats = new List<Chat>(ChatsBuffer);
            }
        }
    }   
}

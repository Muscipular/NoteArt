using System;
using System.Windows;
using GalaSoft.MvvmLight.Messaging;

namespace NoteArt.View.Controls
{
    public class WindowEx : Window, IDisposable
    {
        protected IMessenger MessengerInstance { get { return Messenger.Default; } }
        public void Register<TMessage>(Action<TMessage> action)
        {
            MessengerInstance.Register(this, action);
        }

        public void Register<TMessage>(object token, Action<TMessage> action)
        {
            MessengerInstance.Register(this, token, action);
        }

        public void Register<TMessage>(object token, bool receiveDerivedMessagesToo, Action<TMessage> action)
        {
            MessengerInstance.Register(this, token, receiveDerivedMessagesToo, action);
        }

        public void Register<TMessage>(bool receiveDerivedMessagesToo, Action<TMessage> action)
        {
            MessengerInstance.Register(this, receiveDerivedMessagesToo, action);
        }

        public void Send<TMessage>(TMessage message)
        {
            MessengerInstance.Send(message);
        }

        public void Send<TMessage, TTarget>(TMessage message)
        {
            MessengerInstance.Send<TMessage, TTarget>(message);
        }

        public void Send<TMessage>(TMessage message, object token)
        {
            MessengerInstance.Send(message, token);
        }

        public void Unregister()
        {
            MessengerInstance.Unregister(this);
        }

        public void Unregister<TMessage>()
        {
            MessengerInstance.Unregister<TMessage>(this);
        }

        public void Unregister<TMessage>(object token)
        {
            MessengerInstance.Unregister<TMessage>(this, token);
        }

        public void Unregister<TMessage>(Action<TMessage> action)
        {
            MessengerInstance.Unregister(this, action);
        }

        public void Unregister<TMessage>(object token, Action<TMessage> action)
        {
            MessengerInstance.Unregister(this, token, action);
        }

        public WindowEx()
        {
        }

        public void Dispose()
        {
            MessengerInstance.Unregister(this);
        }
    }
}

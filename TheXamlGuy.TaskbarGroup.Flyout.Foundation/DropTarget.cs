using Microsoft.Xaml.Interactivity;
using System;
using TheXamlGuy.TaskbarGroup.Core;
using Windows.ApplicationModel.DataTransfer;
using Windows.UI.Xaml;

namespace TheXamlGuy.TaskbarGroup.Flyout.Foundation
{
    public class DropTarget : Behavior<FrameworkElement>
    {
        public static readonly DependencyProperty MediatorProperty =
            DependencyProperty.Register(nameof(Mediator),
                typeof(IMediator), typeof(DropTarget),
                new PropertyMetadata(null));

        public IMediator Mediator
        {
            get => (IMediator)GetValue(MediatorProperty);
            set => SetValue(MediatorProperty, value);
        }

        protected override void OnAttached()
        {
            AssociatedObject.DragOver -= OnDragOver;
            AssociatedObject.Drop -= OnDrop;

            AssociatedObject.DragOver += OnDragOver;
            AssociatedObject.Drop += OnDrop;

            base.OnAttached();
        }

        private object CreateDragMessage(object sender, DragEventArgs args)
        {
            var dropMessageType = typeof(Drag<>).MakeGenericType(sender.GetType());
            return Activator.CreateInstance(dropMessageType, args);
        }

        private object CreateDropMessage(object sender, DragEventArgs args)
        {
            var dropMessageType = typeof(Drop<>).MakeGenericType(sender.GetType());
            return Activator.CreateInstance(dropMessageType, args);
        }

        private void OnDragOver(object sender, DragEventArgs args)
        {
            if (Mediator is not null)
            {
                var message = CreateDragMessage(sender, args);
                Mediator.HandleAsync(message);
            }
        }

        private void OnDrop(object sender, DragEventArgs args)
        {
            if (Mediator is not null)
            {
                var message = CreateDropMessage(sender, args);
                Mediator.HandleAsync(message);
            }
        }
    }
}

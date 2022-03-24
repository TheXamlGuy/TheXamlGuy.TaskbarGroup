using Microsoft.Xaml.Interactivity;
using System;
using TheXamlGuy.TaskbarGroup.Core;
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

        protected override void OnDetaching()
        {
            AssociatedObject.DragOver -= OnDragOver;
            AssociatedObject.Drop -= OnDrop;

            base.OnDetaching();
        }

        private void OnDragOver(object sender, DragEventArgs args)
        {
            if (Mediator is not null)
            {
                Mediator.Handle(Activator.CreateInstance(typeof(Drag<>).MakeGenericType(sender.GetType()), args));
            }
        }

        private void OnDrop(object sender, DragEventArgs args)
        {
            if (Mediator is not null)
            {
                Mediator.Handle(Activator.CreateInstance(typeof(Drop<>).MakeGenericType(sender.GetType()), args));
            }
        }
    }
}

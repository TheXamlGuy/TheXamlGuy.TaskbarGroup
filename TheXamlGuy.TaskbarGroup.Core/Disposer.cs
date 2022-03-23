using System.Reactive.Linq;
using System.Reactive.Disposables;
using System.Runtime.CompilerServices;
using System.Collections;

namespace TheXamlGuy.TaskbarGroup.Core
{

    public class Disposer : IDisposer
    {
        private readonly ConditionalWeakTable<object, CompositeDisposable> subjects = new();

        public void Add(object subject, params object[] objects)
        {
            var disposables = subjects.GetOrCreateValue(subject);

            foreach (var disposable in objects.OfType<IDisposable>())
            {
                disposables.Add(disposable);
            }

            foreach (var notDisposable in objects.Where(x => x is not IDisposable))
            {
                disposables.Add(Disposable.Create(() => MakeNotDisposable(notDisposable)));
            }
        }

        public void Dispose(object subject)
        {
            if (subjects.TryGetValue(subject, out CompositeDisposable disposables))
            {
                disposables.Dispose();
            }
        }

        public void Remove(object subject, IDisposable disposer)
        {
            var disposables = subjects.GetOrCreateValue(subject);
            if (disposer != null)
            {
                disposables.Remove(disposer);
            }
        }

        public TDisposable Replace<TDisposable>(object subject, IDisposable disposer, TDisposable replacement) where TDisposable : IDisposable
        {
            var disposables = subjects.GetOrCreateValue(subject);
            if (disposer is not null)
            {
                disposables.Remove(disposer);
            }

            disposables.Add(replacement);
            return replacement;
        }

        private void MakeNotDisposable(object target)
        {
            if (target is IEnumerable enumerableTarget)
            {
                foreach (var item in enumerableTarget)
                {
                    MakeNotDisposable(item);
                }
            }

            if (target is IDisposable disposableTarget)
            {
                disposableTarget.Dispose();
            }

            if (target is not IDisposable)
            {
                Dispose(target);
            }
        }
    }
}

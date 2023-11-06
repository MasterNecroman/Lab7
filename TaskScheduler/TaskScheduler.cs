using System;
using System.Collections.Generic;

namespace Task_4
{
    public class TaskScheduler<TTask, TPriority>
    {
        private List<Tuple<TTask, TPriority>> taskQueue = new List<Tuple<TTask, TPriority>>();
        private Func<TTask, TPriority> getPriorityFunc;
        private Func<TTask> initializeTaskFunc;
        private Action<TTask> resetTaskAction;

        public TaskScheduler(Func<TTask, TPriority> getPriorityFunc, Func<TTask> initializeTaskFunc, Action<TTask> resetTaskAction)
        {
            this.getPriorityFunc = getPriorityFunc;
            this.initializeTaskFunc = initializeTaskFunc;
            this.resetTaskAction = resetTaskAction;
        }

        public void AddTask(TTask task)
        {
            TPriority priority = getPriorityFunc(task);
            taskQueue.Add(Tuple.Create(task, priority));
            taskQueue.Sort((x, y) => Comparer<TPriority>.Default.Compare(x.Item2, y.Item2));
        }

        public void ExecuteNext(TaskExecution<TTask> executionDelegate)
        {
            if (taskQueue.Count > 0)
            {
                Tuple<TTask, TPriority> nextTask = taskQueue[taskQueue.Count - 1];
                taskQueue.RemoveAt(taskQueue.Count - 1);
                TTask task = nextTask.Item1;
                executionDelegate(task);
            }
        }

        public TTask InitializeTask()
        {
            return initializeTaskFunc();
        }

        public void ResetTask(TTask task)
        {
            resetTaskAction(task);
        }

        public void AddTaskFromConsole()
        {
            Console.WriteLine("Enter a task:");
            TTask task = initializeTaskFunc();
            AddTask(task);
        }
    }

    public delegate void TaskExecution<TTask>(TTask task);
}
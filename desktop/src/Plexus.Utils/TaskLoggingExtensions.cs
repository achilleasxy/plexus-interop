/**
 * Copyright 2017 Plexus Interop Deutsche Bank AG
 * SPDX-License-Identifier: Apache-2.0
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */
﻿using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace Plexus
{
    public static class TaskLoggingExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void IgnoreAwait(this Task task, ILogger log)
        {
            task.IgnoreAwait(log, "Exception in non-awaited task");
        }

        [StringFormatMethod("message")]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void IgnoreAwait(this Task task, ILogger log, string message)
        {
            task.LogExceptions(log, message);
        }

        [StringFormatMethod("message")]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void IgnoreAwait<T1>(this Task task, ILogger log, string message, T1 arg1)
        {
            task.LogExceptions(log, message, arg1);
        }

        [StringFormatMethod("message")]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void IgnoreAwait<T1, T2>(this Task task, ILogger log, string message, T1 arg1, T2 arg2)
        {
            task.LogExceptions(log, message, arg1, arg2);
        }

        [StringFormatMethod("message")]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void IgnoreAwait<T1, T2, T3>(this Task task, ILogger log, string message, T1 arg1, T2 arg2, T3 arg3)
        {
            task.LogExceptions(log, message, arg1, arg2, arg3);
        }

        [StringFormatMethod("message")]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task LogExceptions(this Task task, ILogger log, string message)
        {
            if (task.Status == TaskStatus.RanToCompletion)
            {
                return task;
            }

            void LogExceptionsOnCompletion(Task t)
            {
                if (t.Exception == null)
                {
                    return;
                }

                LogExceptionsOnCompletionInternal(t, log, message);
            }

            task.ContinueWithSynchronously((Action<Task>)LogExceptionsOnCompletion);

            return task;
        }

        [StringFormatMethod("message")]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task LogExceptions<T1>(this Task task, ILogger log, string message, T1 arg1)
        {
            if (task.Status == TaskStatus.RanToCompletion)
            {
                return task;
            }

            void LogExceptionsOnCompletion(Task t)
            {
                if (t.Exception == null)
                {
                    return;
                }

                LogExceptionsOnCompletionInternal(t, log, message, arg1);
            }

            task.ContinueWithSynchronously((Action<Task>)LogExceptionsOnCompletion);

            return task;
        }

        [StringFormatMethod("message")]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task LogExceptions<T1, T2>(this Task task, ILogger log, string message, T1 arg1, T2 arg2)
        {
            if (task.Status == TaskStatus.RanToCompletion)
            {
                return task;
            }

            void LogExceptionsOnCompletion(Task t)
            {
                if (t.Exception == null)
                {
                    return;
                }

                LogExceptionsOnCompletionInternal(t, log, message, arg1, arg2);
            }

            task.ContinueWithSynchronously((Action<Task>)LogExceptionsOnCompletion);

            return task;
        }

        [StringFormatMethod("message")]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task LogExceptions<T1, T2, T3>(this Task task, ILogger log, string message, T1 arg1, T2 arg2, T3 arg3)
        {
            if (task.Status == TaskStatus.RanToCompletion)
            {
                return task;
            }

            void LogExceptionsOnCompletion(Task t)
            {
                if (t.Exception == null)
                {
                    return;
                }

                LogExceptionsOnCompletionInternal(t, log, message, arg1, arg2, arg3);
            }

            task.ContinueWithSynchronously((Action<Task>)LogExceptionsOnCompletion);

            return task;
        }

        [StringFormatMethod("message")]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void LogExceptionsOnCompletionInternal(Task t, ILogger log, string message)
        {
            foreach (var innerException in t.Exception.InnerExceptions)
            {
                log.Error(innerException, message);
            }
        }

        [StringFormatMethod("message")]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void LogExceptionsOnCompletionInternal<T1>(Task t, ILogger log, string message, T1 arg1)
        {
            foreach (var innerException in t.Exception.InnerExceptions)
            {
                log.Error(innerException, message, arg1);
            }
        }

        [StringFormatMethod("message")]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void LogExceptionsOnCompletionInternal<T1, T2>(Task t, ILogger log, string message, T1 arg1, T2 arg2)
        {
            foreach (var innerException in t.Exception.InnerExceptions)
            {
                log.Error(innerException, message, arg1, arg2);
            }
        }

        [StringFormatMethod("message")]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void LogExceptionsOnCompletionInternal<T1, T2, T3>(Task t, ILogger log, string message, T1 arg1, T2 arg2, T3 arg3)
        {
            foreach (var innerException in t.Exception.InnerExceptions)
            {
                log.Error(innerException, message, arg1, arg2, arg3);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task LogCompletion(this Task task, ILogger log)
        {
            if (task.IsCompleted)
            {
                LogCompletionInternal(task, log);
            }
            return task.ContinueWithSynchronously(LogCompletionInternal, log);
        }

        private static Task LogCompletionInternal(Task task, object state)
        {
            var log = (ILogger)state;
            if (task.IsCanceled)
            {
                log.Debug("Canceled");
            }
            else if (task.IsFaulted)
            {
                log.Debug("Faulted: {0}", task.Exception.ExtractInner().FormatTypeAndMessage());
            }
            else
            {
                log.Debug("Completed");
            }
            return task;
        }
    }
}

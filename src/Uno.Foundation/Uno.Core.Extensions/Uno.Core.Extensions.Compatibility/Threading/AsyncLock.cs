#nullable enable
// ******************************************************************
// Copyright � 2015-2018 Uno Platform Inc. All rights reserved.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//      http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
// ******************************************************************
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Uno.Threading;

/// <summary>
/// An asynchronous lock, that can be used in conjuction with C# async/await
/// </summary>
internal sealed class AsyncLock
{
	private readonly SemaphoreSlim _semaphore = new(1, 1);
	private long _handleId;

	/// <summary>
	/// Acquires the lock, then provides a disposable to release it.
	/// WARNING: This DOES NOT support reentrancy.
	/// </summary>
	/// <param name="ct">A cancellation token to cancel the lock</param>
	/// <returns>An IDisposable instance that allows the release of the lock.</returns>
	public async ValueTask<Handle> LockAsync(CancellationToken ct)
	{
		await _semaphore.WaitAsync(ct);

		return new Handle(this, _handleId);
	}

	public record struct Handle(AsyncLock Lock, long Id) : IDisposable
	{
		public void Dispose()
		{
			if (Interlocked.CompareExchange(ref Lock._handleId, Id + 1, Id) == Id) // This avoids (concurrent) double dispose / release
			{
				Lock._semaphore.Release();
			}
		}
	}
}

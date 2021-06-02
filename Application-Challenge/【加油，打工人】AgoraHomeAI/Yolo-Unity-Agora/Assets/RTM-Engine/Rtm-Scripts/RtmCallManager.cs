﻿using UnityEngine;
using System.Runtime.InteropServices;
using System;
using AOT;

namespace agora_rtm {
	public sealed class RtmCallManager : IRtmApiNative, IDisposable {
		private IntPtr _rtmCallManagerPtr = IntPtr.Zero;
		private RtmCallEventHandler _rtmCallEventHandler;
		private bool _disposed = false;

		public RtmCallManager(IntPtr rtmCallManager, RtmCallEventHandler rtmCallEventHandler) {
			_rtmCallManagerPtr = rtmCallManager;
			_rtmCallEventHandler = rtmCallEventHandler;
		}

		~RtmCallManager() {
			Dispose(false);
		}

		private void Release() {
			if (_rtmCallManagerPtr == IntPtr.Zero)
			{
				Debug.LogError("_rtmCallManagerPtr is null");
				return;
			}
			rtm_call_manager_release(_rtmCallManagerPtr);
			_rtmCallManagerPtr = IntPtr.Zero;
			if (_rtmCallEventHandler != null) {
				_rtmCallEventHandler.Release();
			}
		}

		/// <summary>
		/// Allows the caller to send a call invitation to the callee.
		/// </summary>
		/// <param name="invitation">An \ref agora_rtm.LocalInvitation "LocalInvitation" object.</param>
		/// <returns>
		///  - 0: Success.
		///  - <0: Failure. See #INVITATION_API_CALL_ERR_CODE for the error code.
		/// </returns>
		public int SendLocalInvitation(LocalInvitation invitation) {
			if (_rtmCallManagerPtr == IntPtr.Zero)
			{
				Debug.LogError("_rtmCallManagerPtr is null");
				return (int)COMMON_ERR_CODE.ERROR_NULL_PTR;
			}
			return rtm_call_manager_sendLocalInvitation(_rtmCallManagerPtr, invitation.GetPtr());
		}
		
		/// <summary>
		/// Allows the callee to accept an incoming call invitation.
		/// </summary>
		/// <param name="invitation">An \ref agora_rtm.RemoteInvitation "RemoteInvitation" object.</param>
		/// <returns>
		///  - 0: Success.
		///  - <0: Failure. See #INVITATION_API_CALL_ERR_CODE for the error code.	
		/// </returns>
		public int AcceptRemoteInvitation(RemoteInvitation invitation) {
			if (_rtmCallManagerPtr == IntPtr.Zero) 
			{
				Debug.LogError("_rtmCallManagerPtr is null");
				return (int)COMMON_ERR_CODE.ERROR_NULL_PTR;
			}
			return rtm_call_manager_acceptRemoteInvitation(_rtmCallManagerPtr, invitation.GetPtr());
		}

		/// <summary>
		/// Allows the callee to decline an incoming call invitation.
		/// </summary>
		/// <param name="invitation">An \ref agora_rtm.RemoteInvitation "RemoteInvitation" object.</param>
		/// <returns>
		///  - 0: Success.
		///  - <0: Failure. See #INVITATION_API_CALL_ERR_CODE for the error code.
		/// </returns>
		public int RefuseRemoteInvitation(RemoteInvitation invitation) {
			if (_rtmCallManagerPtr == IntPtr.Zero) 
			{
				Debug.LogError("_rtmCallManagerPtr is null");
				return (int)COMMON_ERR_CODE.ERROR_NULL_PTR;
			}
			return rtm_call_manager_refuseRemoteInvitation(_rtmCallManagerPtr, invitation.GetPtr());
		}

		/// <summary>
		/// Allows the caller to cancel an outgoing call invitation.
		/// </summary>
		/// <param name="invitation">An \ref agora_rtm.LocalInvitation "LocalInvitation" object.</param>
		/// <returns>
	    ///  - 0: Success.
        ///  - <0: Failure. See #INVITATION_API_CALL_ERR_CODE for the error code.
		/// </returns>
		public int CancelLocalInvitation(LocalInvitation invitation) {
			if (_rtmCallManagerPtr == IntPtr.Zero) 
			{
				Debug.LogError("_rtmCallManagerPtr is null");
				return (int)COMMON_ERR_CODE.ERROR_NULL_PTR;
			}
			return rtm_call_manager_cancelLocalInvitation(_rtmCallManagerPtr, invitation.GetPtr());
		}

		/// <summary>
		/// Creates an \ref agora_rtm.LocalInvitation "LocalInvitation" object.
		/// </summary>
		/// <param name="calleeId">	The Callee's user ID.</param>
		/// <returns>
		/// An \ref agora_rtm.LocalInvitation "LocalInvitation" object.
		/// </returns>
		public LocalInvitation CreateLocalCallInvitation(string calleeId) {
			if (_rtmCallManagerPtr == IntPtr.Zero) 
			{
				Debug.LogError("_rtmCallManagerPtr is null");
				return null;
			}
			return new LocalInvitation(rtm_call_manager_createLocalCallInvitation(_rtmCallManagerPtr, calleeId));
		}
        

        /// <summary>
		/// Releases all resources used by the #RtmCallManager instance.
		/// </summary>
		public void Dispose() {
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		private void Dispose(bool disposing) {
			if (_disposed) return;
			if (disposing) {}
			Release();
			_disposed = true;
		}
	}
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BasicAI {

	public abstract class BaseState {
		public abstract Type Tick();

		public virtual void OnEnter() { }
		public virtual void OnExit() { }
	}

}

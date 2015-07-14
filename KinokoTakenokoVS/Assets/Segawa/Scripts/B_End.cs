using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class B_End : StateMachineBehaviour {

	 // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

        GameObject.Find("SubTitle").GetComponent<Animator>().SetTrigger("Skip");

        GameObject.Find("Title").GetComponent<Animator>().SetTrigger("Skip");

        Debug.Log("test");

        GameObject.Find("TitleLogic").GetComponent<TitleLogic>().endFlag = true;

        GameObject.Find("PushStartText").GetComponent<Text>().enabled = true;

        GameObject.Find("PushStartText").GetComponent<Animator>().SetTrigger("Twinkle");

	}

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	//override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	//override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
	//override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
	//override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}
}

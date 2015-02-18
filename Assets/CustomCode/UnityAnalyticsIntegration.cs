using UnityEngine;
using System.Collections;
using UnityEngine.Cloud.Analytics;

public class UnityAnalyticsIntegration : MonoBehaviour
{
	
	// Use this for initialization
	void Start ()
	{
		
		const string projectId = "4ac00acd-7347-4ea2-b532-8b89b1d0f9ac";
		UnityAnalytics.StartSDK (projectId);
		
	}
	
}
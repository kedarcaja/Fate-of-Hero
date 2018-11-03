using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshJumper : MonoBehaviour {

    private IEnumerator Start()
    {
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        agent.autoTraverseOffMeshLink = false;
        while (true)
        {
            if (agent.isOnOffMeshLink)
            {
                yield return StartCoroutine(Parabola(agent, 2f,0.5f));
                agent.CompleteOffMeshLink();
            }
            yield return null;
        }
    }

    IEnumerator Parabola(NavMeshAgent agent, float height, float duration)
    {
        OffMeshLinkData data = agent.currentOffMeshLinkData;
        Vector3 startPos = agent.transform.position;
        Vector3 endPos = data.endPos;
        float time = 0f;
        while (time < 1f)
        {
            float yOffset = height * (time - time * time);
            agent.transform.position = Vector3.Lerp(startPos, endPos, time) + yOffset * Vector3.up;
            time += Time.deltaTime / duration;
            yield return null;
        }
    }
}

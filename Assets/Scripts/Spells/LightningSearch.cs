using Mono.Cecil;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEditor.PlayerSettings;

[RequireComponent(typeof(ParticleSystem))]
public class LightningSearch : MonoBehaviour
{
    List<Vector3> targets = new List<Vector3>();

    /// <summary>
    /// Returns a list of targets positions starting from the closest, targets need colliders
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="startPos">starting point for search</param>
    /// <param name="range">radius of search</param>
    /// <param name="jumps">How many targets method will try to return</param>
    /// <returns></returns>
    public List<Vector3> SearchForTargetNextTargetIsStartPos<T>(Vector2 startPos, float range, int jumps) where T : MonoBehaviour
    {
        ResetSearch();

        for (int i = 0; i < jumps; i++)
        {
            Vector2 startJumpPos = Vector2.zero;
            Collider2D[] hits;
            if (i == 0)
            {
                startJumpPos = startPos;
                hits = Physics2D.OverlapCircleAll(startJumpPos, range);
            }
            else
            {
                if (i - 1 < targets.Count)
                {
                    startJumpPos = targets[i - 1];
                    hits = Physics2D.OverlapCircleAll(startJumpPos, range);
                }
                else
                    return targets;
            }

            List<Vector3> potentialTargets = new List<Vector3>();

            foreach (var hit in hits)
            {
                if (hit.TryGetComponent(out T target) && !targets.Contains(target.transform.position))
                {
                    potentialTargets.Add(hit.transform.position);
                }
            }

            if(potentialTargets.Count > 0)
            {
                targets.Add(SortListByDistance(potentialTargets, startJumpPos).First());
            }

        }
        return targets;
    }

    private List<Vector3> SortListByDistance(List<Vector3> list, Vector2 startPos)
    {
        list = list.OrderBy(x => Vector3.Distance(startPos, x)).ToList();
        return list;
    }

    private void ResetSearch()
    {
        targets.Clear();
    }
}

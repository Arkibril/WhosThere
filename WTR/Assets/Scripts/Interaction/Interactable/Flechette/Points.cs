using UnityEngine;

public class Points : MonoBehaviour
{

    public int point;
    public Fleche _fleche;

    private void OnTriggerEnter(Collider other)
    {

        if (other.name == "Dart_A" || other.name == "Dart_B" || other.name == "Dart_C")
        {
            _fleche = other.GetComponent<Fleche>();
            _fleche.point += point;           
        }
    }

}

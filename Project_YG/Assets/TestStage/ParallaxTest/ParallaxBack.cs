using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]//게임이 실행되지 않아도 작동함
public class ParallaxBack : MonoBehaviour
{
    public ParallaxCam parallaxCamera;//효과를 적용시킬 카메라 참조
    List<ParallaxC> parallaxLayers = new List<ParallaxC>();//패럴랙스 효과를 넣을 레이어 리스트

    void Start()
    {
        if (parallaxCamera == null)
            parallaxCamera = Camera.main.GetComponent<ParallaxCam>();//parallaxCamera가 설정되지 않은 경우 컴포넌트 가져오기
 
        if (parallaxCamera != null)
            parallaxCamera.onCameraTranslate += Move;//parallaxCamera가 유효한 경우 Move 메소드 호출 
 
        SetLayers();//SetLayers()를 호출해 ParallaxLayer을 찾아 리스트에 추가하기
    }
 
    void SetLayers()
    {
        parallaxLayers.Clear();//기존 레이어 리스트 초기화 
 
        for (int i = 0; i < transform.childCount; i++)
        {
            //자식객체에서 Parallax 컴포넌트 찾기
            ParallaxC layer = transform.GetChild(i).GetComponent<ParallaxC>();
            
            //Parallax 컴포넌트가 있으면 리스트에 추가
            if (layer != null)
            {
                layer.name = "Layer-" + i;
                parallaxLayers.Add(layer);
            }
        }
    }
 
    void Move(float delta)
    {
        //각 레이어를 카메라 이동에 맞춰 이동시키기
        foreach (ParallaxC layer in parallaxLayers)
        {
            layer.Move(delta);
        }
    }
}

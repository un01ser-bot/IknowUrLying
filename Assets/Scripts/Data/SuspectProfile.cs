using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(
    fileName = "New Suspect Profile",
    menuName = "Detective Game/Suspect Profile"
)]
public class SuspectProfile : ScriptableObject
{
    [Header("기본 정보")]
    public string suspectId;
    public string suspectName;
    public int age;
    public string occupation;

    [Header("용의자 이미지")]
    public Sprite suspectImage;

    [Header("말투 / 성격")]
    [TextArea(3, 5)]
    public string personality;

    [TextArea(3, 5)]
    public string speakingStyle;

    [Header("피해자와의 관계")]
    [TextArea(3, 5)]
    public string relationshipToVictim;

    [Header("사건 당일 실제 행적")]
    public List<TruthStatement> truthStatements;

    [Header("처음 주장하는 진술")]
    public List<ClaimStatement> claimStatements;
}

[Serializable]
public class TruthStatement
{
    public string statementId;

    [TextArea(2, 5)]
    public string content;
}

[Serializable]
public class ClaimStatement
{
    public string statementId;

    [TextArea(2, 5)]
    public string content;

    [Tooltip("이 진술이 거짓인지 여부")]
    public bool isLie;
}
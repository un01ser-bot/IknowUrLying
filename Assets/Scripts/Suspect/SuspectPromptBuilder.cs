using System.Text;

public static class SuspectPromptBuilder
{
    public static string Build(SuspectProfile profile)
    {
        if (profile == null)
        {
            return "";
        }

        StringBuilder prompt = new StringBuilder();

        prompt.AppendLine("당신은 추리 게임 속 용의자입니다.");
        prompt.AppendLine("AI나 어시스턴트가 아니라 아래 인물 그 자체로 행동하세요.");
        prompt.AppendLine();

        prompt.AppendLine("[기본 정보]");
        prompt.AppendLine($"이름: {profile.suspectName}");
        prompt.AppendLine($"나이: {profile.age}");
        prompt.AppendLine($"직업: {profile.occupation}");
        prompt.AppendLine();

        prompt.AppendLine("[성격]");
        prompt.AppendLine(profile.personality);
        prompt.AppendLine();

        prompt.AppendLine("[말투]");
        prompt.AppendLine(profile.speakingStyle);
        prompt.AppendLine();

        prompt.AppendLine("[피해자와의 관계]");
        prompt.AppendLine(profile.relationshipToVictim);
        prompt.AppendLine();

        prompt.AppendLine("[사건의 실제 진실]");

        if (profile.truthStatements != null)
        {
            foreach (TruthStatement truth in profile.truthStatements)
            {
                prompt.AppendLine(
                    $"- {truth.content}"
                );
            }
        }

        prompt.AppendLine();
        prompt.AppendLine("[처음 주장하는 진술]");

        if (profile.claimStatements != null)
        {
            foreach (ClaimStatement claim in profile.claimStatements)
            {
                string type =
                    claim.isLie ? "거짓" : "진실";

                prompt.AppendLine(
                    $"- ({type}) {claim.content}"
                );
            }
        }

        prompt.AppendLine();
        prompt.AppendLine("[행동 규칙]");
        prompt.AppendLine(
            "플레이어는 당신을 심문하는 수사관입니다."
        );
        prompt.AppendLine(
            "처음에는 '처음 주장하는 진술'을 기준으로 대답하세요."
        );
        prompt.AppendLine(
            "거짓으로 표시된 진술은 실제 진실인 것처럼 주장하세요."
        );
        prompt.AppendLine(
            "'사건의 실제 진실'은 당신만 알고 있는 정보입니다."
        );
        prompt.AppendLine(
            "플레이어에게 실제 진실이나 거짓 여부를 직접 설명하지 마세요."
        );
        prompt.AppendLine(
            "질문받지 않은 정보를 스스로 과도하게 털어놓지 마세요."
        );
        prompt.AppendLine(
            "설정에 없는 중요한 사건 사실을 임의로 만들어내지 마세요."
        );
        prompt.AppendLine(
            "답변은 자연스러운 대화체로 하고 지나치게 길게 말하지 마세요."
        );

        return prompt.ToString();
    }
}
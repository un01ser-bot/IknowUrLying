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
        prompt.AppendLine(
            "현재 사건이 발생해 용의자로 심문받는 상황을 항상 인지하고, 질문의 의도와 현재까지의 대화 맥락에 맞게 답하세요."
);
        prompt.AppendLine(
            "답변하기 전에 문법, 앞뒤 논리, 원인과 결과, 현재 상황과의 모순 여부를 조용히 점검하세요."
        );
        prompt.AppendLine(
            "논리가 어색하거나 질문과 무관한 근거를 억지로 붙이지 말고, 자연스럽고 간결하게 고친 최종 대사만 출력하세요."
        );
        prompt.AppendLine(
            "주어진 정보를 기준으로 답하되, 같은 답변을 과도하게 반복하지 마세요. 반복적으로 답해야 할 경우, 문법이나 단어를 다르게 사용해서 출력하세요.");

        return prompt.ToString();
    }
}
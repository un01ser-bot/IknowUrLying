using System;
using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class EliceChatClient : MonoBehaviour
{
    [Header("API 설정")]
    [SerializeField] private ApiSecrets apiSecrets;

    private const string BaseUrl =
        "https://mlapi.run/4bbd0c4d-bf02-4e59-a635-457b1c30c56a/v1";

    private const string Model =
        "openai/gpt-4.1-mini";


    public void SendSuspectMessage(
    SuspectProfile profile,
    string playerMessage,
    Action<string> onSuccess)
    {
        if (profile == null)
        {
            Debug.LogWarning(
                "SuspectProfile이 없습니다."
            );
            return;
        }

        string systemPrompt =
            SuspectPromptBuilder.Build(profile);

        StartCoroutine(
            SendChatRequest(
                systemPrompt,
                playerMessage,
                onSuccess
            )
        );
    }


    private IEnumerator SendChatRequest(
        string systemPrompt,
        string userMessage,
        Action<string> onSuccess)
    {
        if (apiSecrets == null)
        {
            Debug.LogError(
                "ApiSecrets가 연결되지 않았습니다."
            );
            yield break;
        }

        if (string.IsNullOrWhiteSpace(apiSecrets.ApiKey))
        {
            Debug.LogError(
                "API Key가 비어 있습니다."
            );
            yield break;
        }

        ChatRequest requestData =
            new ChatRequest
            {
                model = Model,
                messages = new ChatMessage[]
                {
                    new ChatMessage
                    {
                        role = "system",
                        content = systemPrompt
                    },
                    new ChatMessage
                    {
                        role = "user",
                        content = userMessage
                    }
                }
            };

        string json =
            JsonUtility.ToJson(requestData);

        byte[] bodyRaw =
            Encoding.UTF8.GetBytes(json);

        using UnityWebRequest request =
            new UnityWebRequest(
                BaseUrl + "/chat/completions",
                "POST"
            );

        request.uploadHandler =
            new UploadHandlerRaw(bodyRaw);

        request.downloadHandler =
            new DownloadHandlerBuffer();

        request.SetRequestHeader(
            "Content-Type",
            "application/json"
        );

        request.SetRequestHeader(
            "Authorization",
            "Bearer " + apiSecrets.ApiKey
        );

        yield return request.SendWebRequest();

        if (request.result !=
            UnityWebRequest.Result.Success)
        {
            Debug.LogError(
                $"API 요청 실패\n" +
                $"HTTP: {request.responseCode}\n" +
                $"{request.error}\n" +
                $"{request.downloadHandler.text}"
            );

            yield break;
        }

        ChatResponse response =
            JsonUtility.FromJson<ChatResponse>(
                request.downloadHandler.text
            );

        if (response == null ||
            response.choices == null ||
            response.choices.Length == 0)
        {
            Debug.LogError(
                "API 응답에 choices가 없습니다.\n" +
                request.downloadHandler.text
            );

            yield break;
        }

        string answer =
            response.choices[0].message.content;

        onSuccess?.Invoke(answer);
    }
}

[Serializable]
public class ChatRequest
{
    public string model;
    public ChatMessage[] messages;
}

[Serializable]
public class ChatMessage
{
    public string role;
    public string content;
}

[Serializable]
public class ChatResponse
{
    public ChatChoice[] choices;
}

[Serializable]
public class ChatChoice
{
    public ChatMessage message;
}
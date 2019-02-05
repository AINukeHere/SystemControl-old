public class Greater_Int : Operator<BigInt,bool?>
{
    public override void SetDefaultText()
    {
        textMesh.text = ">";
    }
    public override void CheckOutput()
    {
        if (input[0] != null && input[1] != null)
        {
            result[0] = input[0] > input[1];
            output[0].Input(result[0]);
            input[0] = null;
            input[1] = null;
        }
    }
    public override string GetInfoString()
    {
        return "입력된 첫번째 정수값이 두번째 실수값보다 큰지 비교하여 참 또는 거짓을 내보냅니다.";
    }
}

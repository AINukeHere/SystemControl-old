public class Less_Float : Operator<float?,bool?> {
    public override void CheckOutput()
    {
        if (input[0] != null && input[1] != null)
        {
            result[0] = input[0].Value < input[1].Value;
            output[0].Input(result[0]);
            input[0] = null;
            input[1] = null;
        }
    }
}

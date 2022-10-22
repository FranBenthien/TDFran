namespace back.Services;

using Model;
using System;
public class TokenService
{
    public int TokenSize {get; private set; }
    public TokenService(int TokenSize)
    {
        this.TokenSize = TokenSize;
    }
    public async Task<Token> CreateToken (Usuario user)
    {
        int seed = getSeed(user);
        string str = generateRandomString(seed, TokenSize);

        Token token = new Token();

        token.UserId = user.Id;
        token.Value = str;

        using TDSabadoContext context = new TDSabadoContext();
        context.Tokens.Add(token);
        await context.SaveChangesAsync();

        return token;
    }

    private int count = 0;
    private int getSeed(Usuario user)
    {   
        count++;
        var now = DateTime.Now;
        int timeSeed = 
            now.Second * 100 * 100 *100 *100 +
            now.Minute * 100 * 100 * 100 +
            now.Hour * 100 * 100 +
            now.Month * 100 + 
            now.Day;
        int userSeed = 
            (int)user.Name[0] + 
            (int)user.Name[1] +
            (int)user.Name[2];
        int seed = timeSeed * userSeed * count;
        return seed;
    }
    private string generateRandomString(int seed, int n)
    {
        Random rand = new Random(seed);
        char [] array = new char [N];
        for (int i = 0; i < N; i++)
        {
            char c = (char)('a' + rand.Next(26));
            array[i] = c;
        }
        return string.Concat(array);
    }

    public Usuario TokenValidation(Token token)
    {

    }
}
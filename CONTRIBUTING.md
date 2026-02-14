# Contributing to HybridPaymentGateway

Thank you for your interest in contributing to HybridPaymentGateway! 🎉

## How to Contribute

### 1. Fork the Repository

Click the "Fork" button at the top right of the repository page.

### 2. Clone Your Fork

```bash
git clone https://github.com/YOUR_USERNAME/HybridPaymentGateway.git
cd HybridPaymentGateway
```

### 3. Create a Branch

```bash
git checkout -b feature/your-feature-name
```

Branch naming conventions:
- `feature/` - New features
- `bugfix/` - Bug fixes
- `docs/` - Documentation updates
- `refactor/` - Code refactoring

### 4. Make Your Changes

#### Backend (.NET)
- Follow C# coding conventions
- Add XML documentation to public methods
- Write unit tests for new features
- Ensure all tests pass: `dotnet test`

#### Frontend (Angular)
- Follow Angular style guide
- Use TypeScript strict mode
- Add unit tests for components/services
- Ensure tests pass: `npm test`

### 5. Commit Your Changes

Use clear and descriptive commit messages:

```bash
git add .
git commit -m "feat: add Bitcoin address validation"
```

Commit message format:
- `feat:` - New feature
- `fix:` - Bug fix
- `docs:` - Documentation only
- `style:` - Code style changes
- `refactor:` - Code refactoring
- `test:` - Adding tests
- `chore:` - Maintenance tasks

### 6. Push to Your Fork

```bash
git push origin feature/your-feature-name
```

### 7. Create a Pull Request

1. Go to the original repository
2. Click "New Pull Request"
3. Select your fork and branch
4. Fill in the PR template
5. Submit!

## Code Style Guidelines

### .NET (C#)
- Use PascalCase for class names and public members
- Use camelCase for private fields
- Use meaningful variable names
- Follow SOLID principles
- Keep methods small and focused

```csharp
// Good
public class PaymentService
{
    private readonly IPaymentRepository _repository;
    
    public async Task<Payment> CreatePaymentAsync(CreatePaymentRequest request)
    {
        // Implementation
    }
}
```

### Angular (TypeScript)
- Use PascalCase for classes
- Use camelCase for variables and methods
- Use kebab-case for file names
- Follow Angular style guide

```typescript
// Good
export class PaymentService {
  private apiUrl = environment.apiUrl;
  
  getPayments(): Observable<Payment[]> {
    return this.http.get<Payment[]>(`${this.apiUrl}/api/payments`);
  }
}
```

## Testing

### Backend Tests
```bash
cd backend
dotnet test
```

All tests must pass before submitting a PR.

### Frontend Tests
```bash
cd frontend
npm test
```

## Pull Request Guidelines

- **One feature per PR** - Keep PRs focused
- **Tests required** - Include tests for new features
- **Documentation** - Update README if needed
- **Clean commits** - Squash if necessary
- **No breaking changes** - Unless discussed first

## PR Checklist

Before submitting your PR, ensure:

- [ ] Code follows the style guidelines
- [ ] All tests pass
- [ ] New code has tests
- [ ] Documentation is updated
- [ ] Commit messages are clear
- [ ] No merge conflicts
- [ ] PR description is complete

## Questions?

- Open an issue for discussion
- Join our discussions
- Check existing issues/PRs first

## Code Review Process

1. Maintainers will review your PR
2. Feedback may be provided
3. Make requested changes
4. Once approved, it will be merged

## License

By contributing, you agree that your contributions will be licensed under the MIT License.

---

Thank you for contributing! 🙏

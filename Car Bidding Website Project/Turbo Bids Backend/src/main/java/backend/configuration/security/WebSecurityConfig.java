package backend.configuration.security;

import backend.configuration.security.auth.AuthenticationRequestFilter;
import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;
import org.springframework.http.HttpMethod;
import org.springframework.security.config.annotation.method.configuration.EnableMethodSecurity;
import org.springframework.security.config.annotation.web.builders.HttpSecurity;
import org.springframework.security.config.annotation.web.configuration.EnableWebSecurity;
import org.springframework.security.web.AuthenticationEntryPoint;
import org.springframework.security.web.SecurityFilterChain;
import org.springframework.security.web.authentication.UsernamePasswordAuthenticationFilter;
import org.springframework.security.config.annotation.web.configurers.AbstractHttpConfigurer;
import org.springframework.security.config.http.SessionCreationPolicy;
import org.springframework.web.servlet.config.annotation.CorsRegistry;
import org.springframework.web.servlet.config.annotation.WebMvcConfigurer;

import static org.springframework.security.config.Customizer.withDefaults;

@Configuration
@EnableWebSecurity
@EnableMethodSecurity(jsr250Enabled = true)
public class WebSecurityConfig {

    private static final String[] SWAGGER_UI_RESOURCES = {
            "/v3/api-docs/**",
            "/swagger-resources/**",
            "/swagger-ui.html",
            "/swagger-ui/**"};

    private static final String VEHICLES_PATTERN = "/vehicles/**";
    private static final String REQUESTS_PATTERN = "/requests/**";
    private static final String IMAGES_PATTERN = "/images/**";

    @Bean
    public SecurityFilterChain filterChain(HttpSecurity httpSecurity,
                                           AuthenticationEntryPoint authenticationEntryPoint,
                                           AuthenticationRequestFilter authenticationRequestFilter) throws Exception {
        httpSecurity
                .csrf(AbstractHttpConfigurer::disable)
                .formLogin(AbstractHttpConfigurer::disable)
                .sessionManagement(sessionManagement ->
                        sessionManagement.sessionCreationPolicy(SessionCreationPolicy.STATELESS))
                .authorizeHttpRequests(authorizeRequests ->
                        authorizeRequests
                                .requestMatchers(HttpMethod.OPTIONS, "/**").permitAll()
                                .requestMatchers(HttpMethod.POST, VEHICLES_PATTERN).permitAll()
                                .requestMatchers(HttpMethod.GET, VEHICLES_PATTERN).permitAll()
                                .requestMatchers(HttpMethod.DELETE, VEHICLES_PATTERN).permitAll()
                                .requestMatchers(HttpMethod.POST, "/users").permitAll()
                                .requestMatchers(HttpMethod.GET, "/users/**").permitAll()
                                .requestMatchers(HttpMethod.DELETE, "/users/**").permitAll()
                                .requestMatchers(HttpMethod.POST, "/auth/**").permitAll()
                                .requestMatchers(HttpMethod.GET, REQUESTS_PATTERN).permitAll()
                                .requestMatchers(HttpMethod.POST, REQUESTS_PATTERN).permitAll()
                                .requestMatchers(HttpMethod.DELETE, REQUESTS_PATTERN).permitAll()
                                .requestMatchers(HttpMethod.PUT, "/requests/**").permitAll()
                                .requestMatchers(HttpMethod.PUT, "/requests/accept/**").permitAll()
                                .requestMatchers(HttpMethod.PUT, "/requests/decline/**").permitAll()
                                .requestMatchers(HttpMethod.GET, "/listings/open").permitAll()  // Ensure this is public
                                .requestMatchers(HttpMethod.GET, "/listings/**").permitAll()
                                .requestMatchers(HttpMethod.POST, "/listings/**").permitAll()
                                .requestMatchers(HttpMethod.POST, IMAGES_PATTERN).permitAll()
                                .requestMatchers(HttpMethod.GET, IMAGES_PATTERN).permitAll()
                                .requestMatchers(HttpMethod.GET, "/ws/**").permitAll()
                                .requestMatchers(HttpMethod.GET, "/notifications/**").permitAll()
                                .requestMatchers(HttpMethod.PUT, "/notifications/**").permitAll()
                                .anyRequest().authenticated()
                )
                .exceptionHandling(exceptionHandling ->
                        exceptionHandling.authenticationEntryPoint(authenticationEntryPoint))
                .addFilterBefore(authenticationRequestFilter, UsernamePasswordAuthenticationFilter.class)
                .cors(withDefaults()); // Enable CORS with default settings
        return httpSecurity.build();
    }

    @Bean
    public WebMvcConfigurer corsConfigurer() {
        return new WebMvcConfigurer() {
            @Override
            public void addCorsMappings(CorsRegistry registry) {
                registry.addMapping("/**")
                        .allowedOrigins("http://localhost:5173")
                        .allowedMethods("GET", "POST", "PUT", "DELETE", "OPTIONS")
                        .allowedHeaders("*")
                        .allowCredentials(true);
            }
        };
    }
}
